using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MiRS.UI.Wasm.Gateway.MiRSAdmin;
using MiRS.UI.Wasm.Infrastructure.MiRSAdmin;
using MiRS.UI.Wasm.Services;
using Plk.Blazor.DragDrop;

namespace MiRS.UI.Wasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddBlazorDragDrop();

            builder.Services.AddScoped<EventService>();
            builder.Services.AddScoped<TeamService>();
            builder.Services.AddScoped<UsersService>();
            builder.Services.AddScoped<BrowserStorageService>();
            builder.Services.AddScoped<IMiRSEventClient, MiRSEventClient>();
            builder.Services.AddScoped<IMiRSTeamClient, MiRSTeamClient>();
            builder.Services.AddScoped<IMiRSUserClient, MiRSUserClient>();

            await builder.Build().RunAsync();
        }
    }
}
