using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using MyIoTService.Infrastructure.EF;
using MyIoTService.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.TestHost;
using MyIoTService.Core.Services.Mqtt;
using MyIoTService.Tests.Integration.Mocks;
using MQTTnet.Extensions.ManagedClient;

namespace MyIoTService.Tests.Integration
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices((ctx, services) =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<MyIoTDbContext>));

                services.Remove(descriptor);

                services.AddDbContext<MyIoTDbContext>(options =>
                {
                    options.UseSqlServer("Server=localhost,1433;Database=iot_tests;User=sa;Password=Your_password123;", o => o.MigrationsAssembly("MyIoTService.Infrastructure"));
                });
            });

            builder.ConfigureTestServices(services =>
            {
                services.AddTransient<IHiveMqCredentialsService, MockHiveMqCredentialsService>();
                services.AddTransient<IManagedMqttClient, MockManagedMqttClient>();
            });

        }

        protected override void Dispose(bool disposing)
        {
            using(var scope = this.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<MyIoTDbContext>();
                if (db is { })
                {
                    db.Database.EnsureDeleted();
                }

            }
            base.Dispose(disposing);
        }
    }
}
