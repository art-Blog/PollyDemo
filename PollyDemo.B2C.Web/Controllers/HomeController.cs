using System;
using System.Web.Mvc;
using PollyDemo.B2C.Core;
using PollyDemo.B2C.Core.Module;

namespace PollyDemo.B2C.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly Lazy<IMyModule> _greetModule = new Lazy<IMyModule>(ModuleFactory.GetMyModule);
        protected IMyModule MyModule => this._greetModule.Value;

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Hello()
        {
            return Content(MyModule.RetryHello());
        }

        public ActionResult GoodBye()
        {
            return Content(MyModule.Fusing("art"));
        }
    }
}