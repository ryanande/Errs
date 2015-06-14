namespace Errs.WebUi.Infrastructure.Pipelines
{
    using System.Linq;
    using FluentValidation;
    using Logging;
    using MediatR;
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
            _logger.ForContext<TRequest>();

            _logger.Debug("Validating Request");
            var failures = _messageValidator.Validate(message);
            if (failures.Any())
            {
                _logger.Debug("Failed Validation: {failures}", failures);
                throw new ValidationException(failures);
            }

            _logger.Debug("Begin Handle Request");
            var result = _inner.Handle(message);
            _logger.Debug("Request Complete");

            _logger.FlushContext();

            return result;
        }
    }
}