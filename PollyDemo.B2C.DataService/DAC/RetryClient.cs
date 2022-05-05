using System.Net.Http;
using Polly;

namespace PollyDemo.B2C.DataService.DAC
{
    /// <summary>
    /// 僅提供 retry 策略
    /// </summary>
    public class RetryClient
    {
        private static Policy _currentPolicyWrap;
        private static Policy CurrentPolicyWrap => _currentPolicyWrap ?? (_currentPolicyWrap = PolicyFactory.GetInstance<RetryClient>());

        public string Hello()
        {
            return CurrentPolicyWrap.Execute(() =>
            {
                var wsUrl = "https://localhost:44371/api/values";

                var client = new HttpClient();
                var response = client.GetAsync(wsUrl).Result;
                return response.Content.ReadAsStringAsync().Result;
            });
        }
    }
}