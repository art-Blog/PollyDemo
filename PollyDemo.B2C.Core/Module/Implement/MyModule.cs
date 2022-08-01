using System;
using PollyDemo.B2C.DataService;
using PollyDemo.B2C.DataService.DAI;

namespace PollyDemo.B2C.Core.Module.Implement
{
    public class MyModule : IMyModule
    {
        private readonly Lazy<IMyDAO> _myDAO = new Lazy<IMyDAO>(DataFactory.GetMyDAO);

        protected IMyDAO MyDAO => _myDAO.Value;


        public string RetryHello()
        {
            return MyDAO.RetryHello();
        }

        public string Fusing(string name)
        {
            return MyDAO.Fusing(name);
        }

        public string DownGradeHello()
        {
            return MyDAO.DownGradeHello();
        }

        public string CallWebService()
        {
            return MyDAO.CallWebService();
        }
    }
}