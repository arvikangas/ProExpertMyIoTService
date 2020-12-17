using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet;
using MQTTnet.Extensions.ManagedClient;
using MyIoTService.Core.Options;
using MyIoTService.Core.Services;
using MyIoTService.Core.Services.Mqtt;
using System.Reflection;

namespace MyIoTService.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddTransient<IManagedMqttClient>((provider) => new MqttFactory().CreateManagedMqttClient());
            services.AddSingleton<IMqttService, MqttService>();
            services.AddTransient<IHiveMqCredentialsService, HiveMqCredentialsService>();

            services.AddHostedService<BackgroundServiceStarter<IMqttService>>();
            services.AddHostedService<Initializer>();
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddHttpContextAccessor();



            return services;
        }
    }
}
