using larc.Service.Google_Services;
using larc.Web.Components.Base;
using System.Web.Mvc;

namespace larc.Web.Components.Events
{
    public class EventController : BaseController
    {

        public ActionResult GetEvents()
        {
            //var events = uow.Events.GetEventsDto().ToList();
            var events = CalendarGoogleService.GetCalendarEvents();
            return Json(events, JsonRequestBehavior.AllowGet);
        }
    }
}