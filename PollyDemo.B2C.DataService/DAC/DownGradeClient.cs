using System;
using System.Diagnostics;
using System.Net.Http;
using Polly;

namespace PollyDemo.B2C.DataService.DAC
{
    public class DownGradeClient
    {
        private static Policy<string> _currentPolicyWrap;

        private static Policy<string> CurrentPolicyWrap => _currentPolicyWrap ?? (_currentPolicyWrap = GetFallbackPolicy());

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

        private static Policy<string> GetFallbackPolicy()
        {
            Action<DelegateResult<string>, Context> onFallback = (e, c) => Debug.WriteLine("發生錯誤，服務降級");

            return Policy<string>.Handle<Exception>().Fallback<string>("執行失敗，服務降級", onFallback);
        }
    }
}