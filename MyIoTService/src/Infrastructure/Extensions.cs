using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyIoTService.Infrastructure.EF;
using System;

namespace MyIoTService.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<MyIoTDbContext>(options =>
                options.UseSqlServer("Server=localhost,1433;Database=iot;User=sa;Password=Your_password123;", o => o.MigrationsAssembly("MyIoTService.Infrastructure")));

            return services;
        }
    }
}
