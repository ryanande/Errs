namespace Errs.WebUi.Features.Home
{
    using System.Web.Mvc;
    using MediatR;

    public class HomeController : BaseController
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public ActionResult Index()
        {
            var errors = _mediator.Send(new ErrorsRequest { Count = 25 });
            return View(errors);
        }
    }
}