using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyIoTService.Core.Repositories;
using MyIoTService.Domain;
using MyIoTService.Infrastructure.EF;
using MyIoTService.Infrastructure.EF.Repositories;
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

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IAccountDeviceRepository, AccountDeviceRepository>();
            services.AddScoped<IDeviceDataIncomingRepository, DeviceDataIncomingRepository>();
            services.AddScoped<IDeviceDataOutgoingRepository, DeviceDataOutgoingRepository>();

            return services;
        }
    }
}
