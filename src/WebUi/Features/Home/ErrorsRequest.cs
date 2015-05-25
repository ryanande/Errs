namespace Errs.WebUi.Features.Home
{
    using System.Collections.Generic;
    using MediatR;

    public class ErrorsRequest : IRequest<IEnumerable<ErrorsResponse>>
    {
        public int Count { get; set; }
    }
}