namespace Errs.WebUi.Features.Errors
{
    using System.Web.Mvc;

    public class ErrorController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
        public ActionResult NotAuthorized()
        {
            return View();
        }
    }
}