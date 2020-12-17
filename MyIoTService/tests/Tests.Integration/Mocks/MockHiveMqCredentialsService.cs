using MyIoTService.Core.Services.Mqtt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyIoTService.Tests.Integration.Mocks
{
    public class MockHiveMqCredentialsService : IHiveMqCredentialsService
    {
        public Task AddCredentials(string user, string password)
        {
            return Task.CompletedTask;
        }

        public Task DeleteCredentials(string user)
        {
            return Task.CompletedTask;
        }

        public Task EditCredentials(string user, string password)
        {
            return Task.CompletedTask;
        }
    }
}
