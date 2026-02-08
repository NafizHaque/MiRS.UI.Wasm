using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
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

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddBlazorDragDrop();

            builder.Services.AddWebServices();
            builder.Services.AddMiRSClients();

            await builder.Build().RunAsync();
        }
    }
}
