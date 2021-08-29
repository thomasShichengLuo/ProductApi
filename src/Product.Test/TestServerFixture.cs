using System.IO;
using System.Net.Http;
using Flurl.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Product.Api;

namespace Framecad.Nexa.MyFramecad.Tests
{
    public class TestServerFixture : WebApplicationFactory<Startup>
    {

        protected override IHostBuilder CreateHostBuilder()
        {
            var hostBuilder = base.CreateHostBuilder()
                .UseEnvironment("Testing")
                .ConfigureAppConfiguration(builder =>
                {
                    var configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                    builder.AddJsonFile(configPath);
                });

            return hostBuilder;
        }


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(collection =>
            {

            });
            base.ConfigureWebHost(builder);
        }
    }
}