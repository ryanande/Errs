namespace Errs.WebUi.Infrastructure.Pipelines
{
    using System.Linq;
    using System.Runtime.InteropServices;
    using FluentValidation;
    using MediatR;
    using Serilog;
    using Validation;
    using ILogger = Logging.ILogger;

    public class RequestPipeline<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;
        private readonly IMessageValidator<TRequest> _messageValidator;
        private readonly ILogger _logger;

        public RequestPipeline(IRequestHandler<TRequest, TResponse> inner, IMessageValidator<TRequest> messageValidator, ILogger logger)
        {
            _inner = inner;
            _messageValidator = messageValidator;
            _logger = logger;
        }

        public TResponse Handle(TRequest message)
        {
            var str = "Handler executing for command: {@" + typeof (TRequest).Name + "}";
            _logger.Debug(str, message);

            var failures = _messageValidator.Validate(message);
            if (failures.Any())
            {
                _logger.Debug("Failed Validation: {@Failures}", failures);
                throw new ValidationException(failures);
            }

            _logger.Debug("Calling Handle Request");
            var result = _inner.Handle(message);
            _logger.Debug("Completed Handle Request");

            return result;
        }
    }
}