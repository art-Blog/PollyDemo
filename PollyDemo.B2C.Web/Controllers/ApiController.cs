using System;
using System.Diagnostics;
using System.Web.Mvc;
using Polly;
using PollyDemo.B2C.Core;
using PollyDemo.B2C.Core.Module;
using PollyDemo.Common.DataClass.Entity;
using PollyDemo.Common.DataClass.Entity.Demo;

namespace PollyDemo.B2C.Web.Controllers
{
    public class ApiController : Controller
    {
        private readonly Lazy<IMyModule> _myModule = new Lazy<IMyModule>(ModuleFactory.GetMyModule);
        protected IMyModule MyModule => this._myModule.Value;

        [HttpGet]
        public ActionResult Retry()
        {
            string result;
            try
            {
                result = MyModule.RetryHello();
            }
            catch (Exception e)
            {
                result = $"停止重試:{e.Message}";
            }

            return Content(result);
        }

        [HttpGet]
        public ActionResult DownGrade()
        {
            string result;
            try
            {
                result = MyModule.DownGradeHello();
            }
            catch (Exception e)
            {
                result = $"發生錯誤，服務降級:{e.Message}";
            }

            return Content(result);
        }


        [HttpPost]
        public ActionResult Fusing(DemoRequest request)
        {
            string result;
            try
            {
                result = MyModule.Fusing(request.Name);
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return Content(result);
        }
    }
}