using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services
{
    //ovdje stvaramo token, jwt - we don't store anything in database
    //we generate token from our server, our server will sign this token
    //one key is used to both encript and decript signature in our token
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
        }

        public string CreateToken(AppUser user)
        {
            //each user will have list of claims inside token
            //claim is information about user, for example user has date of birth and he claims the date is
            //this and this
            var claims = new List<Claim>
            {
                //claims for email and name
                //we must not putany sensitive information in claims
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.DisplayName)
            };
            //alghoritm, the strength of encription - this hmac is the largest level of encription
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //this describes content of our token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                //issuer dodaješ u appsetings.development.json
                Issuer = _config["Token:Issuer"]
            };
            //this will handle our token
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}