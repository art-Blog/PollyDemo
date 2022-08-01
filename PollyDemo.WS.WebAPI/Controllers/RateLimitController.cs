using System;
using System.Web.Http;
using System.Web.Http.Cors;
using Polly;
using Polly.RateLimit;
using PollyDemo.WS.WebAPI.MyPolicy;

namespace PollyDemo.WS.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RateLimitController : ApiController
    {
        private static Policy _currentPolicy;
        private static Policy CurrentPolicy => _currentPolicy ?? (_currentPolicy = PolicyFactory.RateLimitPolicy());

        [HttpPost]
        public string Post()
        {
            var timeStamp = DateTime.Now;

            try
            {
                return CurrentPolicy.Execute(() => DoSomething(timeStamp));
            }
            catch (RateLimitRejectedException ex)
            {
                return ex.Message;
            }
        }


        private static string DoSomething(DateTime timeStamp)
        {
            return "This is a POST response from the RateLimitController - " + timeStamp;
        }
    }
}