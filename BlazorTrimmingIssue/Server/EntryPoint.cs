using System.IO;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BlazorTrimmingIssue.Server
{
    public static class EntryPoint
    {
        public static void Main()
        {
            using var host = new HostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureHostConfiguration(builder => builder
                    .AddEnvironmentVariables("DOTNET_")
                    .AddJsonFile("appsettings.json"))
                .ConfigureLogging(builder => builder
                    .AddConsole())
                .ConfigureWebHost(builder => builder
                    .ConfigureAppConfiguration((context, builder) => StaticWebAssetsLoader
                        .UseStaticWebAssets(context.HostingEnvironment, context.Configuration))
                    .UseKestrel()
                    .UseStartup<Startup>())
                .Build();

            host.Run();
        }
    }
}
