using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using PollyDemo.Common.DataClass.Entity;
using PollyDemo.Common.DataClass.Entity.Demo;

namespace PollyDemo.WS.WebAPI.Controllers
{
    [EnableCors(origins:"*",headers:"*",methods:"*")]
    public class ValuesController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public IEnumerable<string> Post([FromBody] DemoRequest request)
        {
            return new string[] { request.Name, };
        }
    }
}