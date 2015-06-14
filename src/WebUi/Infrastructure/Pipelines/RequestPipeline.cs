namespace Errs.WebUi.Infrastructure.Pipelines
{
    using System.Linq;
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
            // _logger.ForContext("Begin Handler: {0}", message);
            var log = Log.ForContext<TRequest>();

            var str = "message: {@" + typeof (TRequest).Name + "}";
            

            log.Debug(str, message);

            // _logger.Debug("Validating Request");
            var failures = _messageValidator.Validate(message);
            if (failures.Any())
            {
                // _logger.Debug("Failed Validation: {failures}", failures);
                log.Debug("Failed Validation: {failures}", failures);
                throw new ValidationException(failures);
            }

            log.Debug("Start Handle Request");
            // _logger.Debug("Start Handle Request");
            var result = _inner.Handle(message);
            // _logger.Debug("Request Complete");
            log.Debug("Completed Handle Request");

            return result;
        }
    }
}