using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MyIoTService.Tests.Integration
{
    [Collection("sequence1")]
    public class HomeControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        [Fact]
        public async Task homecontroller_should_respond()
        {
            var resp = await _client.GetAsync("/home");
            resp.ShouldNotBeNull();
            resp.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        }

        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;

        public HomeControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                BaseAddress = new Uri(@"http://localhost:5001")
            });
        }
    }
}
