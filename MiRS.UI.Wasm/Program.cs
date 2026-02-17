using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MiRs.Domain.Configurations;
using MiRS.UI.Wasm.Infrastructure;
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

            builder.Services.Configure<AppSettings>(
                builder.Configuration.GetSection("Api"));

            IConfigurationSection azureApi = builder.Configuration.GetSection("AzureApi");

            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAdExternalId", options.ProviderOptions.Authentication);

                // Add API scope
                options.ProviderOptions.DefaultAccessTokenScopes.Add($"api://{azureApi["ClientId"]}/External.User.Access");

            });

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddBlazorDragDrop();

            builder.Services.AddAuthorizationCore();

            builder.Services.AddWebServices();
            builder.Services.AddMiRSClients();

            await builder.Build().RunAsync();
        }
    }
}
