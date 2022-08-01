using System.Net.Http;
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

        public string CallWebService()
        {
            return new HttpClient().PostAsync(
                "https://localhost:44371/api/rateLimit",
                new StringContent("{\"name\":\"test\"}")
            ).Result.Content.ReadAsStringAsync().Result;
        }
    }
}