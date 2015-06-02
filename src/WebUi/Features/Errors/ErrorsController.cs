namespace Errs.WebUi.Features.Errors
{
    using System.Web.Mvc;

    public class ErrorsController : BaseController
    {

        public ActionResult Index()
        {
            return View(ViewData.Model);
        }

        public ActionResult NotFound()
        {
            return View(ViewData.Model);
        }
        public ActionResult NotAuthorized()
        {
            return View(ViewData.Model);
        }
    }
}