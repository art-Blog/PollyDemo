using PollyDemo.B2C.DataService.DAI;
using PollyDemo.B2C.DataService.DAO;

namespace PollyDemo.B2C.DataService
{
    public static class DataFactory
    {
        public static IMyDAO GetMyDAO()
        {
            return new MyDAO();
        }
    }
}