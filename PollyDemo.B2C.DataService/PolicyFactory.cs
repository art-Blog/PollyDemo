using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Polly;
using Polly.Timeout;
using PollyDemo.Common.DataClass.Entity.Policy;

namespace PollyDemo.B2C.DataService
{
    public static class PolicyFactory
    {
        public static Policy GetInstance<T>()
        {
            var config = GetPolicyConfig<T>();
            return config.PolicyWrap.Length == 1
                ? GetPolicy<T>(config.PolicyWrap.First(), config)
                : GetPolicyWrap<T>(config);
        }

        /// <summary>
        /// 取得多項策略
        /// </summary>
        /// <param name="config">設定</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static Policy GetPolicyWrap<T>(PolicyConfig config)
        {
            var policies =
                config.PolicyWrap
                    .Select(name => GetPolicy<T>(name, config))
                    .Where(policy => policy != null)
                    .ToList();

            if (policies.Count == 0) throw new Exception($"No Match PolicyWrap For {typeof(T)}");

            return Policy.Wrap(policies.ToArray());
        }

        /// <summary>
        /// 取得單一策略
        /// </summary>
        /// <param name="policy">策略名稱</param>
        /// <param name="config">設定</param>
        /// <returns></returns>
        private static Policy GetPolicy<T>(string policy, PolicyConfig config)
        {
            // TODO: 假定情況是設定值都有正確設定，之後應補上防呆處理
            switch (policy)
            {
                case "retry":
                    return GetRetryPolicy<T>(config.RetryConfig.RetryLimit);
                case "circuitBreaker":
                    return GetCircuitBreaker<T>(config.CircuitBreakerConfig.ExceptionsAllowedBeforeBreaking, config.CircuitBreakerConfig.DurationOfBreak);
                case "timeout":
                    return GetTimeoutPolicy<T>(config.TimeoutConfig.TimeoutSeconds);
                default:
                    return null;
            }
        }


        private static PolicyConfig GetPolicyConfig<T>()
        {
            return GetPolicyConfigs().Find(x => x.PolicyName == GetPolicyName<T>());
        }

        private static string GetPolicyName<T>()
        {
            var current = GetPolicyLookupConfigs().Find(x => x.ClassName == typeof(T).ToString());
            return current != null ? current.PolicyName : "default";
        }

        private static List<PolicyLookupConfig> GetPolicyLookupConfigs()
        {
            return ReadConfigFile<PolicyLookupConfig>($"{GetBasePath()}\\_config\\policy.lookup.config.json");
        }

        private static List<PolicyConfig> GetPolicyConfigs()
        {
            return ReadConfigFile<PolicyConfig>($"{GetBasePath()}\\_config\\policy.config.json");
        }

        private static string GetBasePath()
        {
            return HttpContext.Current == null
                ? AppDomain.CurrentDomain.BaseDirectory
                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
        }

        private static List<T> ReadConfigFile<T>(string configPath)
        {
            using (var sr = new StreamReader(configPath))
            {
                return JsonConvert.DeserializeObject<List<T>>(sr.ReadToEnd());
            }
        }

        /// <summary>
        /// Consecutive County Circuit Controller
        /// </summary>
        /// <param name="failureThreshold">異常比例，(以 1 表示 100%；0.5表示 50%)</param>
        /// <param name="samplingDuration">取樣時間(秒)</param>
        /// <param name="minimumThroughput">取樣時間內最少要有幾次請求才算</param>
        /// <param name="durationOfBreak">斷路時間(秒)</param>
        /// <returns></returns>
        /// <remarks>採樣時間　samplingDuration 秒內至少有　minimumThroughput　次請求，且異常的發生比例超過多少 failureThreshold %，則斷路 durationOfBreak 秒</remarks>
        private static Policy GetAdvanceCircuitBreaker<T>(double failureThreshold, int samplingDuration, int minimumThroughput, int durationOfBreak)
        {
            var logPrefix = $"[{typeof(T)}]";
            Action<Exception, TimeSpan> onBreak = (exception, timespan) => { Debug.WriteLine($"{logPrefix} AdvanceCircuitBreaker: Open at {DateTime.Now}"); };
            Action onReset = () => { Debug.WriteLine($"{logPrefix} AdvanceCircuitBreaker: Closed at {DateTime.Now}"); };
            Action onHalfOpen = () => { Debug.WriteLine($"{logPrefix} AdvanceCircuitBreaker: Half-Open at {DateTime.Now}"); };

            return Policy
                .Handle<Exception>()
                .AdvancedCircuitBreaker(
                    failureThreshold,
                    TimeSpan.FromSeconds(samplingDuration),
                    minimumThroughput,
                    TimeSpan.FromSeconds(durationOfBreak),
                    onBreak,
                    onReset,
                    onHalfOpen
                );
        }

        /// <summary>
        /// 連續記數斷路器
        /// </summary>
        /// <param name="exceptionsAllowedBeforeBreaking">允許的例外次數</param>
        /// <param name="durationOfBreak">斷路時間(秒)</param>
        /// <returns></returns>
        /// <remarks>連續失敗 exceptionsAllowedBeforeBreaking 次，則斷路 durationOfBreak 秒鐘</remarks>
        private static Policy GetCircuitBreaker<T>(int exceptionsAllowedBeforeBreaking, int durationOfBreak)
        {
            var logPrefix = $"[{typeof(T)}]";
            Action<Exception, TimeSpan> onBreak = (exception, timespan) => { Debug.WriteLine($"{logPrefix} CircuitBreaker: Open at {DateTime.Now}"); };
            Action onReset = () => { Debug.WriteLine($"{logPrefix} CircuitBreaker: Closed at {DateTime.Now}"); };
            Action onHalfOpen = () => { Debug.WriteLine($"{logPrefix} CircuitBreaker: Half-Open at {DateTime.Now}"); };

            return Policy
                .Handle<Exception>()
                .CircuitBreaker(
                    exceptionsAllowedBeforeBreaking,
                    TimeSpan.FromSeconds(durationOfBreak),
                    onBreak,
                    onReset,
                    onHalfOpen
                );
        }

        /// <summary>
        /// Timeout 
        /// </summary>
        /// <param name="timeout">timeout seconds</param>
        /// <returns></returns>
        /// <remarks>執行超過 timeout 秒的請求會直接放棄掉</remarks>
        private static Policy GetTimeoutPolicy<T>(int timeout)
        {
            var logPrefix = $"[{typeof(T)}]";
            Action<Context, TimeSpan, Task> onTimeout = (context, timespan, task) => { Debug.WriteLine($"{logPrefix} 逾時時間:{timespan}"); };

            return Policy.Timeout(TimeSpan.FromSeconds(timeout), TimeoutStrategy.Pessimistic, onTimeout);
        }

        /// <summary>
        /// Retry
        /// </summary>
        /// <param name="count">retry limit</param>
        /// <returns></returns>
        private static Policy GetRetryPolicy<T>(int count)
        {
            var logPrefix = $"[{typeof(T)}]";
            Action<Exception, int> onRetry = (exception, i) => { Debug.WriteLine($"{logPrefix} 開始重試..."); };

            return Policy.Handle<Exception>().Retry(count, onRetry);
        }
    }
}