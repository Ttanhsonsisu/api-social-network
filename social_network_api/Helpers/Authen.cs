using Microsoft.IdentityModel.Tokens;
using social_network_api.Extensions;
using social_network_api.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace social_network_api.Helpers
{
    public class Authen : IJwt
    {
        private readonly string key;

        public Authen(string key)
        {
            this.key = key;
        }

        public string Authentitcation(string username, string password, string userType)
        {
            if (!(username.Equals(username) || password.Equals(password)))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.NameIdentifier, userType)
                    }),
                Expires = DateTime.UtcNow.AddDays(Consts.TIME_EXPIRES),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            return tokenHandler.WriteToken(token);
        }
    }
}
