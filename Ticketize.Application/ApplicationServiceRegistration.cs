using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Ticketize.Application.Features.Events.Commands.CreateEvent;

namespace Ticketize.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var ass = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(CreateEventCommandHandler)));

            return services;
        }
    }
}
