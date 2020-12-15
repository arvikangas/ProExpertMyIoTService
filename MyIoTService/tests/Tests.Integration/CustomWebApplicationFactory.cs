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

namespace MyIoTService.Tests.Integration
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices((ctx, services) =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<MyIoTDbContext>));

                services.AddDbContext<MyIoTDbContext>(options =>
                {
                    options.UseSqlServer("Server=localhost,1433;Database=iot_tests;User=sa;Password=Your_password123;", o => o.MigrationsAssembly("MyIoTService.Infrastructure"));
                });
            });

        }
    }
}
