using System;

namespace PollyDemo.Common.DataClass.Entity.Policy
{
    [Serializable]
    public class TimeoutConfig
    {
        /// <summary>
        /// Timeout 秒數
        /// </summary>
        public int TimeoutSeconds { get; set; }
    }
}