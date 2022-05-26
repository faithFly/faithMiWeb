using FaithMiApplication1.Jwt;
using FaithMiApplication1.Models;
using FaithMiApplication1.Redis;
using FaithMiApplication1.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FaithMiApplication1.Controllers
{
    [Route("api/[controller]")]
    public class AuthorizeController : Controller
    {
        private JwtSettings _jwtSettings;
        private readonly IUsersRepository _usersRepository;
        // 连接Redis客户端
        RedisHelper redisHelper = new RedisHelper("127.0.0.1:6379");
        Result result = null;
        public AuthorizeController(IOptions<JwtSettings> _jwtSettingsAccesser, IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
            _jwtSettings = _jwtSettingsAccesser.Value;
        }
        /// <summary>
        /// 授权登录
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> TokenAsync([FromBody] User viewModel)
        {
            try
            {
                var users = await _usersRepository.LogingUser(viewModel.UserName, viewModel.Password);
                var info="";
                //
                if (ModelState.IsValid)//判断是否合法
                {
                   
                        if (users.LoginCode==0)
                        {
                           return Ok(new { Msg = users.LoginMsg, Code = users.LoginCode });
                         }
                        else
                        {
                       var claims = new[]
                         {
                          new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                          new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"),
                          new Claim(ClaimTypes.Name, users.UserName)
                         };
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Const.SecurityKey));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            issuer: Const.Domain,
                            audience: Const.Domain,
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: creds);
                        if (token!=null)
                        {
                            bool isInsertSucc = redisHelper.SetValue("token", token.ToString());
                            if (isInsertSucc) {
                                info = redisHelper.GetValue("token");
                                
                            }
                        }
                        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), id = users.UserId ,name=users.UserName,redis=info});
                        }
                    
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    
}
}
