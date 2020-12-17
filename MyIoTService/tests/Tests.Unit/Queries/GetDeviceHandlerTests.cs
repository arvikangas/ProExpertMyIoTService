using MediatR;
using Microsoft.AspNetCore.Http;
using MyIoTService.Core.Commands;
using MyIoTService.Core.Commands.Handlers;
using MyIoTService.Core.Dtos;
using MyIoTService.Core.Exceptions;
using MyIoTService.Core.Queries;
using MyIoTService.Core.Queries.Handlers;
using MyIoTService.Core.Repositories;
using MyIoTService.Core.Services.Mqtt;
using MyIoTService.Domain;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyIoTService.Tests.Unit.Handlers
{
    public class GetDeviceHandlerTests
    {
        [Fact]
        public async Task handler_should_return_device_with_correct_data()
        {
            var command = new GetDevice
            {
                Id = "device1",
            };

            var userName = "user";
            var httpContext = new DefaultHttpContext();
            var user = new System.Security.Claims.ClaimsPrincipal();
            user.AddIdentity(new System.Security.Claims.ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Name, userName) }));
            httpContext.User = user;
            _httpContextAccessor.HttpContext.Returns(httpContext);

            var account = new Account() { Id = Guid.NewGuid() };
            _accountRepository.GetByUserName(userName).Returns(account);

            var device = new Device { Id = command.Id };

            var accountDevice = new AccountDevice { AccountId = account.Id, DeviceId = device.Id };
            _accountDeviceRepository.Get((account.Id, device.Id)).Returns(accountDevice);

            _deviceRepository.Get(command.Id).Returns(device);

            var deviceDto = await _handler.Handle(command, new System.Threading.CancellationToken());

            deviceDto.Id.ShouldBe(device.Id);
        }

        [Fact]
        public async Task handler_should_return_null_if_account_has_no_access_to_device()
        {
            var command = new GetDevice
            {
                Id = "device1",
            };

            var userName = "user";
            var httpContext = new DefaultHttpContext();
            var user = new System.Security.Claims.ClaimsPrincipal();
            user.AddIdentity(new System.Security.Claims.ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Name, userName) }));
            httpContext.User = user;
            _httpContextAccessor.HttpContext.Returns(httpContext);

            var account = new Account() { Id = Guid.NewGuid() };
            _accountRepository.GetByUserName(userName).Returns(account);

            var device = new Device { Id = command.Id };

            var accountDevice = new AccountDevice { AccountId = account.Id, DeviceId = device.Id };
            _accountDeviceRepository.Get((account.Id, device.Id)).Returns((AccountDevice)null);

            var deviceDto = await _handler.Handle(command, new System.Threading.CancellationToken());

            deviceDto.ShouldBeNull();
        }


        private readonly IAccountRepository _accountRepository;
        private readonly IAccountDeviceRepository _accountDeviceRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        IRequestHandler<GetDevice, DeviceDto> _handler;


        public GetDeviceHandlerTests()
        {
            _accountRepository = Substitute.For<IAccountRepository>();
            _accountDeviceRepository = Substitute.For<IAccountDeviceRepository>();
            _deviceRepository = Substitute.For<IDeviceRepository>();
            _httpContextAccessor = Substitute.For<IHttpContextAccessor>();

            _handler = new GetDeviceHandler(
                _accountRepository,
                _accountDeviceRepository,
                _deviceRepository,
                _httpContextAccessor);
        }
    }
}
