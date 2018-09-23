using larc.DAL;
using System.Web.Mvc;

namespace larc.Web.Components.Base
{
    public class BaseController : Controller
    {
        public UnitOfWork uow;
        public BaseController()
        {
            uow = new UnitOfWork(new LarcContext());
        }
    }
}