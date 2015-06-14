namespace Errs.WebUi.Infrastructure.Pipelines
{
    using System.Linq;
    using FluentValidation;
    using Logging;
    using MediatR;
    using Validation;

    public class CommandPipeline<TRequest> : RequestHandler<TRequest> where TRequest : IRequest
    {
        private readonly RequestHandler<TRequest> _inner;
        private readonly IMessageValidator<TRequest> _validator;
        private readonly ILogger _logger;

        public CommandPipeline(RequestHandler<TRequest> inner, IMessageValidator<TRequest> validator, ILogger logger)
        {
            _inner = inner;
            _validator = validator;
            _logger = logger;
        }

        protected override void HandleCore(TRequest message)
        {
            _logger.ForContext("Command:", message);
            _logger.Debug("Command Object: {message}", message);

            _logger.Debug("Validating Command");
            var failures = _validator.Validate(message);

            if (failures.Any())
            {
                _logger.Debug("Failed Validation");
                throw new ValidationException(failures);
            }

            _logger.Debug("Begin Handle Command");
            _inner.Handle(message);
            _logger.Debug("Command Complete");

            _logger.FlushContext();
        }
    }
}