namespace Errs.WebUi.Features.Home
{
    using System;
    using System.Web;
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

        public ActionResult OhFour()
        {
            throw new HttpException(404, "Doh!");
        }

        public ActionResult FiveHundy()
        {
            throw new Exception("Fall down go Boom!");
        }
    }
}