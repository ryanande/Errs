namespace Errs.WebUi.Infrastructure.Pipelines
{
    using System.Linq;
    using FluentValidation;
    using MediatR;
    using Serilog;
    using Validation;

    public class RequestPipeline<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;
        private readonly IMessageValidator<TRequest> _messageValidator;

        public RequestPipeline(IRequestHandler<TRequest, TResponse> inner, IMessageValidator<TRequest> messageValidator)
        {
            _inner = inner;
            _messageValidator = messageValidator;
        }

        public TResponse Handle(TRequest message)
        {
            var requestLog = Log.ForContext("Request:", message);

            var failures = _messageValidator.Validate(message);

            requestLog.Information("Validating Request");
            if (failures.Any())
            {
                requestLog.Information("Failed Validation");
                throw new ValidationException(failures);
            }

            requestLog.Information("Begin Handle Request");
            var result = _inner.Handle(message);
            requestLog.Information("Request Complete");

            return result;
        }
    }
}