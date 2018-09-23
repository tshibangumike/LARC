using System;
using System.IO;
using System.Web.Mvc;
using larc.Model;
using larc.Service.Google_Services;
using larc.Web.Components.Base;

namespace larc.Web.Components.Teachings
{
    public class PodcastController : BaseController
    {
        public ActionResult AddPodcast(Podcast podcast)
        {
            if (podcast.Id == Guid.Empty)
            {
                podcast.Id = Guid.NewGuid();
            }
            podcast.AddedOn = DateTime.Now;
            uow.Podcasts.Add(podcast);
            uow.SaveChanges();
            return Json(podcast, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPodcasts()
        {
            try
            {
                //var podcasts = uow.Podcasts.GetLast10();
                var podcasts = CalendarGoogleService.GetCalendarEventsWithPodcasts();
                return Json(podcasts, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                // Append line to the file.
                using (StreamWriter writer =
                    new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Error/log.text"), true))
                {
                    writer.WriteLine(ex);
                }

                return Json(null, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult GetPodcast(Guid id)
        {
            var podcast = uow.Podcasts.GetDto(id);
            return Json(podcast, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdatePodcast(Podcast podcast)
        {
            podcast.ModifiedOn = DateTime.Now;
            uow.Podcasts.Attach(podcast);
            uow.SaveChanges();
            return Json(podcast, JsonRequestBehavior.AllowGet);
        }
    }
}