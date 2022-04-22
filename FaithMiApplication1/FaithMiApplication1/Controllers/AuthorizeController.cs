using FaithMiApplication1.Jwt;
using FaithMiApplication1.Models;
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
           
                //
                if (ModelState.IsValid)//判断是否合法
                {
                   
                        if (users.LoginCode==0)
                        {
                           return Ok(new { Msg = users.LoginMsg, Code = users.LoginCode });
                         }
                        else
                        {
                        var claim = new Claim[]{
                        new Claim(ClaimTypes.Name,viewModel.UserName),
                        new Claim(ClaimTypes.Role,viewModel.Password)
                         };

                            //对称秘钥
                            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                            //签名证书(秘钥，加密算法)
                            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                            //生成token  [注意]需要nuget添加Microsoft.AspNetCore.Authentication.JwtBearer包，并引用System.IdentityModel.Tokens.Jwt命名空间
                            var token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claim, DateTime.Now, DateTime.Now.AddMinutes(5), creds);


                            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), id = users.UserId ,name=users.UserName});
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
