namespace Errs.WebUi.Infrastructure.Pipelines
{
    using System;
    using System.Linq.Expressions;
    using FluentValidation;
    using FluentValidation.Results;
    using Utility;

    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        protected ValidationFailure Success
        {
            get { return null; }
        }

        protected ValidationFailure CreateFailureFor(Expression<Func<T, object>> property, string errorMessage)
        {
            return new ValidationFailure(property.AsPropertyInfo().Name, errorMessage);
        }
    }
}