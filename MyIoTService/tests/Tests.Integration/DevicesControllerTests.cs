using Microsoft.AspNetCore.Mvc.Testing;
using MyIoTService.Core.Commands;
using MyIoTService.Core.Dtos;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace MyIoTService.Tests.Integration
{
    [Collection("sequence1")]
    public class DevicesControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        [Fact]
        public async Task get_devices_should_return_list_of_devices()
        {
            var resp = await _client.GetAsync("/devices");
            resp.ShouldNotBeNull();
            resp.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);

            var dto = await GetResultAsync<List<DeviceDto>>(resp);
            dto.ShouldNotBeNull();
            dto.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task get_device_should_return_device()
        {
            var resp = await _client.GetAsync("/devices/device1");
            resp.ShouldNotBeNull();
            resp.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);

            var dto = await GetResultAsync<DeviceDto>(resp);
            dto.ShouldNotBeNull();
            dto.Id.ShouldBe("device1");
        }

        [Fact]
        public async Task post_device_should_create_device()
        {
            var request = new CreateDevice
            {
                Id = "device3",
                Enabled = true,
                Password = "somepassword"
            };
            var resp = await PostAsync("/devices", request);
            resp.ShouldNotBeNull();
            resp.StatusCode.ShouldBe(System.Net.HttpStatusCode.Created);

            var createdHeader = resp.Headers.Location;
            var locationString = createdHeader.OriginalString;

            var dto = await GetAsync<DeviceDto>(locationString);

            dto.ShouldNotBeNull();
            dto.Id.ShouldBe(request.Id);
        }

        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;

        public DevicesControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                BaseAddress = new Uri(@"http://localhost:5001"),
            });

            var signIn = new SignIn
            {
                UserName = "user",
                Password = "secret"
            }; 

            var result = PostAsync("/accounts/login", signIn).Result;
            var dto = GetResultAsync<TokenDto>(result).Result;

            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", dto.Token);
        }

        private async Task<HttpResponseMessage> PostAsync<T>(string url, T obj)
        {
            var jsonString = JsonSerializer.Serialize(obj);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var result = await _client.PostAsync(url, content);
            return result;
        }

        private async Task<T> GetAsync<T>(string url)
        {
            var result = await _client.GetAsync(url);
            var resultObject = await GetResultAsync<T>(result);
            return resultObject;
        }

        private async Task<T> GetResultAsync<T>(HttpResponseMessage message)
        {
            var resultString = await message.Content.ReadAsStringAsync();
            var resultObject = JsonSerializer.Deserialize<T>(resultString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return resultObject;
        }
    }
}
