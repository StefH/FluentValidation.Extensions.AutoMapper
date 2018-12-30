using FluentValidation;
using FluentValidationExample.Business.Implementations;
using FluentValidationExample.Business.Interfaces.Public;
using FluentValidationExample.Business.Validation;
using FluentValidationExample.Common.Validation;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for setting up localization services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds services required for business logic.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static void AddBusiness([NotNull] this IServiceCollection services)
        {
            Guard.NotNull(services, nameof(services));

            services.AddServices();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<AddressValidator>()
                .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
            );

            services.AddScoped<IPersonService, PersonService>();
        }
    }
}