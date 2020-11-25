using MediatR;
using MyIoTService.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Core.Commands
{
    public class SignIn : IRequest<TokenDto>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
