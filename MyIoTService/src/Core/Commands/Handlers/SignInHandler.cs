using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyIoTService.Core.Commands;
using MyIoTService.Core.Dtos;
using MyIoTService.Core.Options;
using MyIoTService.Domain;
using MyIoTService.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Commands.Handlers
{
    public class SignInHandler : IRequestHandler<SignIn, TokenDto>
    {
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private readonly MyIoTDbContext _db;
        private readonly IOptions<JwtOptions> _jwtoptions;

        public SignInHandler(
            UserManager<Account>  userManager,
            SignInManager<Account> signInManager,
            MyIoTDbContext db,
            IOptions<JwtOptions> jwtoptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
            _jwtoptions = jwtoptions;
        }

        public async Task<TokenDto> Handle(SignIn request, CancellationToken cancellationToken)
        {
            var account = _db.Accounts.FirstOrDefault(x => x.UserName == request.UserName);
            var signInResult = await _signInManager.CheckPasswordSignInAsync(account, request.Password, false);

            if (!signInResult.Succeeded)
            {
                return null;
            }

            var token = GenerateToken(request.UserName);
            return new TokenDto
            {
                Token = token
            };
        }

        private string GenerateToken(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtoptions.Value.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName)
                }),

                Expires = DateTime.UtcNow.AddSeconds(3600),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
