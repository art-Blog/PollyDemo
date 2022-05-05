using System;
using PollyDemo.Common.DataClass.Enums;
using PollyDemo.Common.DataClass.Extension;

namespace PollyDemo.Common.DataClass.Response.Base
{
    [Serializable]
    public abstract class ResponseBase
    {
        protected ResponseBase() : this(WsEnum.ResultCode.Success)
        {
        }

        protected ResponseBase(WsEnum.ResultCode resultCode)
        {
            this.ResultCode = resultCode.GetDescription();
            this.ResultMessage = resultCode.ToString();
        }

        public string ResultCode { get; set; }

        public string ResultMessage { get; set; }
    }
}