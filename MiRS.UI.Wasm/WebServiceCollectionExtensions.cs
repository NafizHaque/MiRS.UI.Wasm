using MiRS.UI.Wasm.Services;

namespace MiRS.UI.Wasm
{
    public static class WebServiceCollectionExtensions
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddScoped<EventService>();
            services.AddScoped<TeamService>();
            services.AddScoped<UsersService>();
            services.AddScoped<AdminOwnerService>();
            services.AddScoped<BrowserStorageService>();

            return services;
        }
    }
}
