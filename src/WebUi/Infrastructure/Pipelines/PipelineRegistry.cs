namespace Errs.WebUi.Infrastructure.Pipelines
{
    using FluentValidation;
    using MediatR;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
    using Utility;
    using Validation;

    public class PipelineRegistry: Registry
    {
        public PipelineRegistry()
        {
            Scan(scan =>
            {
                scan.AssembliesFromApplicationBaseDirectory(a => a.IsLocalAssembly());
                scan.AssemblyContainingType<IMediator>();

                scan.AddAllTypesOf(typeof(IMediator));
                scan.AddAllTypesOf(typeof(IValidator<>));
                scan.AddAllTypesOf(typeof(IRequestHandler<,>));
                scan.WithDefaultConventions();
            });

            For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
            For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
            For(typeof(IRequestHandler<,>)).DecorateAllWith(typeof(RequestPipeline<,>));
            For(typeof(RequestHandler<>)).DecorateAllWith(typeof(CommandPipeline<>));
            For(typeof(IMessageValidator<>)).Use(typeof(FluentValidationMessageValidator<>));
        }
    }
}
