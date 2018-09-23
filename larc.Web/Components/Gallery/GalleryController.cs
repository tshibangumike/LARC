using larc.Service;
using System.Web.Mvc;

namespace larc.Web.Components.Gallery
{
    public class GalleryController : Controller
    {
        [AllowAnonymous]
        public ActionResult GetPhotos()
        {
            var pictures = DriveGoogleService.GetPicturesFromGoogleDrive();
            return Json(pictures, JsonRequestBehavior.AllowGet);
        }
    }
}