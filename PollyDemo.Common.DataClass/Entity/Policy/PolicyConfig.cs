using System;

namespace PollyDemo.Common.DataClass.Entity.Policy
{
    [Serializable]
    public class PolicyConfig
    {
        /// <summary>
        /// 設定檔名稱
        /// </summary>
        public string PolicyName { get; set; }

        /// <summary>
        /// 設定的策略
        /// </summary>
        public string[] PolicyWrap { get; set; }
        
        /// <summary>
        /// Retry 設定
        /// </summary>
        public RetryConfig RetryConfig { get; set; }

        /// <summary>
        /// Timeout 設定
        /// </summary>
        public TimeoutConfig TimeoutConfig { get; set; }

        /// <summary>
        /// 斷路器設定
        /// </summary>
        public CircuitBreakerConfig CircuitBreakerConfig { get; set; }

        /// <summary>
        /// 進階斷路器設定
        /// </summary>
        public AdvancedCircuitBreakerConfig AdvancedCircuitBreakerConfig { get; set; }
    }
}