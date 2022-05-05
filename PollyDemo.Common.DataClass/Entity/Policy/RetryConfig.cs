using System;

namespace PollyDemo.Common.DataClass.Entity.Policy
{
    [Serializable]
    public class RetryConfig
    {
        /// <summary>
        /// 重試次數
        /// </summary>
        public int RetryLimit { get; set; }
    }
}