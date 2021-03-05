using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TheStoreLoginFrontEnd
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            Console.WriteLine($"apiBaseAddress: {builder.Configuration["apiBaseAddress"]}");
            System.Diagnostics.Debug.WriteLine($"apiBaseAddress: {builder.Configuration["apiBaseAddress"]}");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["apiBaseAddress"]) });
            builder.Services.AddScoped<PublicApiService>();

            await builder.Build().RunAsync();
        }
    }
}