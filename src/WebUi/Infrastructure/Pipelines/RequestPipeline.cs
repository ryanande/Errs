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
        private readonly ILogger _logger;

        public RequestPipeline(IRequestHandler<TRequest, TResponse> inner, IMessageValidator<TRequest> messageValidator, ILogger logger)
        {
            _inner = inner;
            _messageValidator = messageValidator;
            _logger = logger;
        }

        public TResponse Handle(TRequest message)
        {
            var requestLog = _logger.ForContext<TRequest>();
            //requestLog.Debug("Request Object: {message}", message);

            requestLog.Debug("Validating Request");
            var failures = _messageValidator.Validate(message);
            if (failures.Any())
            {
                requestLog.Debug("Failed Validation: {failures}", failures);
                throw new ValidationException(failures);
            }

            requestLog.Debug("Begin Handle Request");
            var result = _inner.Handle(message);
            requestLog.Debug("Request Complete");

            return result;
        }
    }
}