using FaithMiApplication1.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FaithMiApplication1.Jwt
{
    public class JwtHelper
    {
        public static string GetToken(string userName) {
            var secret = "1234567890123456";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
            new Claim(ClaimTypes.Name,userName),
            //添加自定义信息
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                "faith@tom.com",
                "user",
                 claims,
                 expires: DateTime.Now.AddMinutes(120),
                 signingCredentials: credentials
           );
            return new JwtSecurityTokenHandler().WriteToken(token);

          }  
    }
}
