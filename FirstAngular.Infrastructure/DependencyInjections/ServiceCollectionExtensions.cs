using FirstAngular.Infrastructure.Behaviors;
using FirstAngular.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Infrastructure.DependencyInjections
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(CurrentUserService).Assembly;
            services.Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes.Where(type =>
                         type.Name.EndsWith("Service") &&
                        !typeof(BackgroundService).IsAssignableFrom(type)))

                .AsImplementedInterfaces()
                .WithScopedLifetime());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthCookieBehavior<,>));

            return services;
        }
    }
}
