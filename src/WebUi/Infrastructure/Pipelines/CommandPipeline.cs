namespace Errs.WebUi.Infrastructure.Pipelines
{
    using System.Linq;
    using FluentValidation;
    using MediatR;
    using Serilog;
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
            var commandLog = _logger.ForContext("Command:", message);
            commandLog.Debug("Command Object: {message}", message);

            commandLog.Debug("Validating Command");
            var failures = _validator.Validate(message);

            if (failures.Any())
            {
                commandLog.Debug("Failed Validation");
                throw new ValidationException(failures);
            }

            commandLog.Debug("Begin Handle Command");
            _inner.Handle(message);
            commandLog.Debug("Command Complete");
        }
    }
}