using FaithMiApplication1.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace FaithMiApplication1.Jwt
{
    public class JwtHelper
    {
        /*public static string BuildToken(User identity)
        {
          *//*  Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var jwtsetting = config.AppSettings.Settings["JwtSetting"].Value;

            //准备calims，随便写，爱写多少写多少，但千万别放敏感信息
            var calims = identity.PropValuesType().Select(x => new Claim(x.Name, x.Value.ToString(), x.Type)).ToList();

            //创建header
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsetting.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(creds);

            //创建payload
            var payload = new JwtPayload(jwtsetting.Issuer, jwtsetting.Audience, calims, DateTime.Now, DateTime.Now.AddMinutes(jwtsetting.ExpireSeconds));

            //创建令牌 
            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);*//*
        }*/
    }
}
