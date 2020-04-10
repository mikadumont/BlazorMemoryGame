using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BlazorMemoryGame.Models;

namespace BlazorMemoryGame
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddBaseAddressHttpClient();
            builder.Services.AddSingleton(new MemoryGameModel(500));

            await builder.Build().RunAsync();
        }
    }
}