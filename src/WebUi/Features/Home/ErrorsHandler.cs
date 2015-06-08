namespace Errs.WebUi.Features.Home
{
    using System.Collections.Generic;
    using Infrastructure.DataAccess;
    using MediatR;
    using WebGrease.Css.Extensions;

    public class ErrorsHandler : IRequestHandler<ErrorsRequest, IEnumerable<ErrorsResponse>>
    {
        public IEnumerable<ErrorsResponse> Handle(ErrorsRequest message)
        {
            var table = new StructuredLog();
            var list = new List<ErrorsResponse>();
            table.All().ForEach(e => list.Add(new ErrorsResponse
            {
                Id = e.Id,
                Message = e.Message,
                MessageTemplate = e.MessageTemplate,
                Exception = e.Exception,
                Level = e.Level,
                Properties = e.Properties,
                TimeStamp = e.TimeStamp
            }));

            return list;
        }
    }
}