using System.ComponentModel;

namespace PollyDemo.Common.DataClass.Enums
{
    public static class WsEnum
    {
        public enum ResultCode
        {
            [Description("0000")]
            Success,
            [Description("0001")]
            InvalidArgument,
            [Description("0002")]
            InMaintain,
            [Description("9999")]
            SystemError
        }

    }
}