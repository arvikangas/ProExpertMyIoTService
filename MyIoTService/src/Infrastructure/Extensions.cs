using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyIoTService.Domain;
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

            services
                .AddIdentity<Account, IdentityRole<Guid>>(options => {
                    options.Password = new PasswordOptions
                    {
                        RequireDigit = false,
                        RequireLowercase = false,
                        RequireUppercase = false,
                        RequireNonAlphanumeric = false,
                    };
                })
                .AddEntityFrameworkStores<MyIoTDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
