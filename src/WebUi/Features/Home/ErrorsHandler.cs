namespace Errs.WebUi.Features.Home
{
    using System;
    using System.Collections.Generic;
    using MediatR;

    public class ErrorsHandler : IRequestHandler<ErrorsRequest, IEnumerable<ErrorsResponse>>
    {
        public IEnumerable<ErrorsResponse> Handle(ErrorsRequest message)
        {
            // test
            return new List<ErrorsResponse>
            {
                new ErrorsResponse
                {
                    Id = 1,
                    Message = "Error Message",
                    MessageTemplate = "Message Template",
                    Exception = "exception",
                    Level = "debug",
                    Properties = "xml properties, need to deserialize",
                    TimeStamp = DateTime.Now
                },
                new ErrorsResponse
                {
                    Id = 2,
                    Message = "Error Message",
                    MessageTemplate = "Message Template",
                    Exception = "exception",
                    Level = "debug",
                    Properties = "xml properties, need to deserialize",
                    TimeStamp = DateTime.Now
                },
                new ErrorsResponse
                {
                    Id = 3,
                    Message = "Error Message",
                    MessageTemplate = "Message Template",
                    Exception = "exception",
                    Level = "debug",
                    Properties = "xml properties, need to deserialize",
                    TimeStamp = DateTime.Now
                },
                new ErrorsResponse
                {
                    Id = 4,
                    Message = "Error Message",
                    MessageTemplate = "Message Template",
                    Exception = "exception",
                    Level = "debug",
                    Properties = "xml properties, need to deserialize",
                    TimeStamp = DateTime.Now
                },
                new ErrorsResponse
                {
                    Id = 5,
                    Message = "Error Message",
                    MessageTemplate = "Message Template",
                    Exception = "exception",
                    Level = "debug",
                    Properties = "xml properties, need to deserialize",
                    TimeStamp = DateTime.Now
                },
                new ErrorsResponse
                {
                    Id = 6,
                    Message = "Error Message",
                    MessageTemplate = "Message Template",
                    Exception = "exception",
                    Level = "debug",
                    Properties = "xml properties, need to deserialize",
                    TimeStamp = DateTime.Now
                }
            };
        }
    }
}