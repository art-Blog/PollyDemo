using System;

namespace PollyDemo.Common.DataClass.Entity.Policy
{
    [Serializable]
    public class AdvancedCircuitBreakerConfig
    {
        /// <summary>
        /// 斷路時間(秒)
        /// </summary>
        public int DurationOfBreak { get; set; }

        /// <summary>
        /// 取樣時間內最少要有幾次請求才算
        /// </summary>
        public int MinimumThroughput { get; set; }

        /// <summary>
        /// 取樣時間(秒)
        /// </summary>
        public int SamplingDuration { get; set; }

        /// <summary>
        /// 異常比例，(以 1 表示 100%；0.5表示 50%)
        /// </summary>
        public double FailureThreshold { get; set; }
    }
}