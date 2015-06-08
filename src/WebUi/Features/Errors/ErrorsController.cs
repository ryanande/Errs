namespace Errs.WebUi.Features.Errors
{
    using System.Web.Mvc;
    using Shared;

    public class ErrorsController : BaseController
    {

        public ActionResult Index()
        {
            return ReturnView(ViewData.Model as ErrorInfo);
        }

        public ActionResult NotFound()
        {
            return ReturnView(ViewData.Model as ErrorInfo);
        }
        public ActionResult UnAuthorized()
        {
            return ReturnView(ViewData.Model as ErrorInfo);
        }

        public ActionResult Forbidden()
        {
            return ReturnView(ViewData.Model as ErrorInfo);
        }

        private ActionResult ReturnView(ErrorInfo model)
        {
            if (model == null)
            {
                return View();
            }

            Response.StatusCode = model.StatusCode;

            if (!model.IsAjaxRequest)
            {
                return View(model);
            }

            var errorObjet = new { message = model.Exception.Message };
            return Json(errorObjet, JsonRequestBehavior.AllowGet);
        }
    }
}