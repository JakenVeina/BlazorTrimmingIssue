using BlazorTrimmingIssue.Shared;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using ProtoBuf.Grpc.Server;

namespace BlazorTrimmingIssue.Server
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCodeFirstGrpc();
            services
                .AddScoped<TestContract>()
                .AddScoped<ITestContract>(serviceProvider => serviceProvider.GetRequiredService<TestContract>());
        }

        public static void Configure(IApplicationBuilder builder)
        {
            builder
                .UseWebAssemblyDebugging();
            builder
                .UseBlazorFrameworkFiles()
                .UseStaticFiles();
            builder
                .UseRouting()
                .UseGrpcWeb(new GrpcWebOptions()
                {
                    DefaultEnabled = true
                })
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapGrpcService<TestContract>();
                })
                .Use(next => context =>
                {
                    context.Request.Path = "/index.html";
                    context.SetEndpoint(null);

                    return next(context);
                })
                .UseStaticFiles();
        }
    }
}
