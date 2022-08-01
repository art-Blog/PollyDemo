namespace PollyDemo.B2C.Core.Module
{
    public interface IMyModule
    {
        string RetryHello();
        string Fusing(string name);
        string DownGradeHello();
        string CallWebService();
    }
}