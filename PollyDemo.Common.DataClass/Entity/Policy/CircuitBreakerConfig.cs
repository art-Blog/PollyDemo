using System;

namespace PollyDemo.Common.DataClass.Entity.Policy
{
    [Serializable]
    public class CircuitBreakerConfig
    {
        /// <summary>
        /// 發生幾次例外之後斷路
        /// </summary>
        public int ExceptionsAllowedBeforeBreaking { get; set; }

        /// <summary>
        /// 斷路時間(秒)
        /// </summary>
        public int DurationOfBreak { get; set; }
    }
}