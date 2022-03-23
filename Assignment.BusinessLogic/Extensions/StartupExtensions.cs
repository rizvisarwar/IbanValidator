using Assignment.BusinessLogic.Features.Shared;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Assignment.BusinessLogic.Extensions
{
    public static class StartupExtensions
    {
        public static string Environment { get; private set; }
        public static string ApplicationName { get; private set; }
        public static void AddBusinessLogicServiceCollection(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            Environment = configuration.GetValue<string>("Environment");
            ApplicationName = configuration.GetValue<string>("ApplicationName");
            serviceCollection.AddMediatRConfiguration();
            serviceCollection.AddFluentValidationConfiguration();
        }

        private static void AddMediatRConfiguration(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
            serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
        }

        private static void AddFluentValidationConfiguration(this IServiceCollection serviceCollection)
        {
            AssemblyScanner
                .FindValidatorsInAssembly(Assembly.GetExecutingAssembly())
                .ForEach(r => serviceCollection.AddScoped(r.InterfaceType, r.ValidatorType));
        }
    }
}
