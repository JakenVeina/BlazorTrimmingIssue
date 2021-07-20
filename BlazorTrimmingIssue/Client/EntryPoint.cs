using System.Net.Http;
using System.Threading.Tasks;

using BlazorTrimmingIssue.Shared;

using Grpc.Net.Client;
using Grpc.Net.Client.Web;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

using ProtoBuf.Grpc.Client;

namespace BlazorTrimmingIssue.Client
{
    public static class EntryPoint
    {
        public static async Task Main(string[] args)
        {
            var hostBuilder = WebAssemblyHostBuilder.CreateDefault(args);

            hostBuilder.RootComponents.Add<ApplicationRoot>("#application-root");

            hostBuilder.Services
                .AddSingleton(serviceProvider => GrpcChannel.ForAddress(
                    hostBuilder.HostEnvironment.BaseAddress,
                    new GrpcChannelOptions
                    {
                        HttpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler())
                    }))
                .AddSingleton(serviceProvider => serviceProvider.GetRequiredService<GrpcChannel>().CreateGrpcService<ITestContract>())
                .AddTransient<ApplicationRootModel>();

            await using var host = hostBuilder.Build();

            await host.RunAsync();
        }
    }
}
