using System;

namespace PollyDemo.Common.DataClass.Entity.Policy
{
    [Serializable]
    public class PolicyLookupConfig
    {
        /// <summary>
        /// 設定檔名稱
        /// </summary>
        public string PolicyName { get; set; }

        /// <summary>
        /// client 類別名稱
        /// </summary>
        public string ClassName { get; set; }
    }
}