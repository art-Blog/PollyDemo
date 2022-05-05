using PollyDemo.B2C.Core.Module;
using PollyDemo.B2C.Core.Module.Implement;

namespace PollyDemo.B2C.Core
{
    public static class ModuleFactory
    {
        public static IMyModule GetMyModule()
        {
            return new MyModule();
        }
    }
}