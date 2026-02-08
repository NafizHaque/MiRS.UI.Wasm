using Microsoft.Extensions.DependencyInjection;
using MiRS.UI.Wasm.Gateway.MiRSAdmin;
using MiRS.UI.Wasm.Infrastructure.MiRSAdmin;

namespace MiRS.UI.Wasm.Infrastructure
{
    public static class ServiceClientCollectionExtensions
    {
        public static IServiceCollection AddMiRSClients(this IServiceCollection services)
        {
            services.AddScoped<IMiRSEventClient, MiRSEventClient>();
            services.AddScoped<IMiRSOwnerClient, MiRSOwnerClient>();
            services.AddScoped<IMiRSTeamClient, MiRSTeamClient>();
            services.AddScoped<IMiRSUserClient, MiRSUserClient>();

            return services;
        }
    }

}
