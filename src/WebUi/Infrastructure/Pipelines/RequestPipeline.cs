﻿namespace Errs.WebUi.Infrastructure.Pipelines
{
    using System.Linq;
    using FluentValidation;
    using MediatR;
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
            var failures = _messageValidator.Validate(message);

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            var result = _inner.Handle(message);

            return result;
        }
    }
}