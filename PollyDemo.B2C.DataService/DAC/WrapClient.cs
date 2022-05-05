using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Polly;
using PollyDemo.Common.DataClass.Entity.Demo;

namespace PollyDemo.B2C.DataService.DAC
{
    /// <summary>
    /// 套用了多種策略的 client
    /// </summary>
    public class WrapClient
    {
        private static Policy _currentPolicyWrap;
        private static Policy CurrentPolicyWrap => _currentPolicyWrap ?? (_currentPolicyWrap = PolicyFactory.GetInstance<WrapClient>());

        public string Fusing(string name)
        {
            var wsUrl = "https://localhost:44371/api/values";
            var request = new DemoRequest() { Name = name };

            var myContent = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return CurrentPolicyWrap.Execute(() =>
            {
                var client = new HttpClient();
                var result = client.PostAsync(wsUrl, byteContent).Result;
                return result.Content.ReadAsStringAsync().Result;
            });
        }
    }
}