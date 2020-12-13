using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyIoTService.Core.Commands;
using MyIoTService.Core.Dtos;
using MyIoTService.Core.Options;
using MyIoTService.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Commands.Handlers
{
    public class CreateAccountHandler : AsyncRequestHandler<CreateAccount>
    {
        private readonly UserManager<Account> _userManager;
        private readonly IOptions<JwtOptions> _jwtoptions;

        public CreateAccountHandler(
            UserManager<Account>  userManager,
            IOptions<JwtOptions> jwtoptions)
        {
            _userManager = userManager;
            _jwtoptions = jwtoptions;
        }

        protected override async Task Handle(CreateAccount request, CancellationToken cancellationToken)
        {
            var account = new Account { UserName = request.UserName };
            var result = await _userManager.CreateAsync(account, request.Password);
        }


        private string GenerateToken(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtoptions.Value.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.UserName.ToString())
                }),

                Expires = DateTime.UtcNow.AddSeconds(3600),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
