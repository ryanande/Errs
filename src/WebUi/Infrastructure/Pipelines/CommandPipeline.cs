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

        public CommandPipeline(RequestHandler<TRequest> inner, IMessageValidator<TRequest> validator)
        {
            _inner = inner;
            _validator = validator;
        }

        protected override void HandleCore(TRequest message)
        {
            var commandLog = Log.ForContext("Command:", message);
            commandLog.Information("Validating Command");

            var failures = _validator.Validate(message);

            if (failures.Any())
            {
                commandLog.Information("Failed Validation");
                throw new ValidationException(failures);
            }

            commandLog.Information("Begin Handle Command");
            _inner.Handle(message);
            commandLog.Information("Command Complete");
        }
    }
}