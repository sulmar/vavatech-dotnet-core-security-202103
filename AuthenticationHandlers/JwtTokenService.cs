using IServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationHandlers
{
    public class JwtTokenServiceOptions
    {
        public string SecretKey { get; set; }
    }

    public class JwtTokenService : ITokenService
    {
        private readonly JwtTokenServiceOptions options;

        public JwtTokenService(IOptions<JwtTokenServiceOptions> options)
        {
            this.options = options.Value;
        }

        // dotnet add package System.IdentityModel.Tokens.Jwt
        public string CreateToken(Customer customer)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            ClaimsIdentity identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(ClaimTypes.Email, customer.Email));
            identity.AddClaim(new Claim(ClaimTypes.MobilePhone, customer.PhoneNumber));

            var tokenHandler = new JwtSecurityTokenHandler();
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = credentials
            };

            SecurityToken securityToken = tokenHandler.CreateToken(descriptor);

            string token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
