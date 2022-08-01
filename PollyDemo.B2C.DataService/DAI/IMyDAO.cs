namespace PollyDemo.B2C.DataService.DAI
{
    public interface IMyDAO
    {
        string RetryHello();
        string Fusing(string name);
        string DownGradeHello();
        string CallWebService();
    }
}