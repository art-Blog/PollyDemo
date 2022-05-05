using System.Web.Http;
using System.Web.Http.Cors;
using PollyDemo.Common.DataClass.Entity;
using PollyDemo.Common.DataClass.Entity.Demo;

namespace PollyDemo.WS.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GreetingController : ApiController
    {
        [HttpPost]
        public DemoResponse Post([FromBody] DemoRequest request)
        {
            return new DemoResponse() { Result = new[] { request.Name } };
        }
    }
}