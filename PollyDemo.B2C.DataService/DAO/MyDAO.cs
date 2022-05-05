using PollyDemo.B2C.DataService.DAC;
using PollyDemo.B2C.DataService.DAI;

namespace PollyDemo.B2C.DataService.DAO
{
    public class MyDAO : IMyDAO
    {
        public string RetryHello()
        {
            return new RetryClient().Hello();
        }

        public string Fusing(string name)
        {
            return new WrapClient().Fusing(name);
        }

        public string DownGradeHello()
        {
            return new DownGradeClient().Hello();
        }
    }
}