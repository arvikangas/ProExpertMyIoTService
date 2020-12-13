using MediatR;
using Microsoft.AspNetCore.Http;
using MyIoTService.Core.Commands;
using MyIoTService.Core.Commands.Handlers;
using MyIoTService.Core.Exceptions;
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
    public class CreateDeviceHandlerTests
    {
        [Fact]
        public async Task handler_should_create_device_with_correct_data()
        {
            var command = new CreateDevice
            {
                Id = "device1",
                Enabled = true,
                Password = "password"
            };

            var userName = "user";
            var httpContext = new DefaultHttpContext();
            var user = new System.Security.Claims.ClaimsPrincipal();
            user.AddIdentity(new System.Security.Claims.ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Name, userName) }));
            httpContext.User = user;
            _httpContextAccessor.HttpContext.Returns(httpContext);

            var account = new Account() { Id = Guid.NewGuid() };
            _accountRepository.GetByUserName(userName).Returns(account);

            _deviceRepository.Get(command.Id).Returns((Device)null);

            await _handler.Handle(command, new System.Threading.CancellationToken());

            await _deviceRepository.Received().Create(Arg.Is<Device>(d =>
                d.Id == command.Id &&
                d.Enabled == command.Enabled));

            await _accountDeviceRepository.Received().Create(Arg.Is<AccountDevice>(ad =>
                ad.DeviceId == command.Id &&
                ad.AccountId == account.Id));

            await _hiveMqCredentialsService.Received().AddCredentials(command.Id, command.Password);
        }

        [Fact]
        public async Task handler_should_throw_exception_with_empty_device_id()
        {
            var command = new CreateDevice
            {
                Id = "",
                Enabled = true,
                Password = "password"
            };

            Should.Throw<DeviceIdEmptyException>(() => _handler.Handle(command, new System.Threading.CancellationToken()));
        }

        [Fact]
        public async Task handler_should_throw_exception_with_empty_device_password()
        {
            var command = new CreateDevice
            {
                Id = "asd",
                Enabled = true,
                Password = ""
            };

            Should.Throw<DevicePasswordEmptyException>(() => _handler.Handle(command, new System.Threading.CancellationToken()));
        }

        [Fact]
        public async Task handler_should_throw_exception_with_existing_device()
        {
            var command = new CreateDevice
            {
                Id = "device1",
                Enabled = true,
                Password = "password"
            };

            var userName = "user";
            var httpContext = new DefaultHttpContext();
            var user = new System.Security.Claims.ClaimsPrincipal();
            user.AddIdentity(new System.Security.Claims.ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Name, userName) }));
            httpContext.User = user;
            _httpContextAccessor.HttpContext.Returns(httpContext);

            var account = new Account() { Id = Guid.NewGuid() };
            _accountRepository.GetByUserName(userName).Returns(account);

            _deviceRepository.Get(command.Id).Returns(new Device());

            Should.Throw<DeviceAlreadyExistsException>(() => _handler.Handle(command, new System.Threading.CancellationToken()));
        }

        private readonly IAccountRepository _accountRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IAccountDeviceRepository _accountDeviceRepository;
        private readonly IMqttService _mqttService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHiveMqCredentialsService _hiveMqCredentialsService;

        IRequestHandler<CreateDevice> _handler;


        public CreateDeviceHandlerTests()
        {
            _accountRepository = Substitute.For<IAccountRepository>();
            _deviceRepository = Substitute.For<IDeviceRepository>();
            _accountDeviceRepository = Substitute.For<IAccountDeviceRepository>();
            _mqttService = Substitute.For<IMqttService>();
            _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            _hiveMqCredentialsService = Substitute.For<IHiveMqCredentialsService>();

            _handler = new CreateDeviceHandler(
                _accountRepository, 
                _deviceRepository,
                _accountDeviceRepository, 
                _mqttService, 
                _httpContextAccessor, 
                _hiveMqCredentialsService);
        }
    }
}
