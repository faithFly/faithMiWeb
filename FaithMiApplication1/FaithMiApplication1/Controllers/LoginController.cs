using FaithMiApplication1.Jwt;
using FaithMiApplication1.Models;
using FaithMiApplication1.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FaithMiApplication1.Controllers
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private JwtSettings _jwtSettings;

        public LoginController(IUsersRepository usersRepository,IOptions<JwtSettings> _jwtSettingsAccesser) { 
            _usersRepository = usersRepository;
            _jwtSettings = _jwtSettingsAccesser.Value;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _usersRepository.GetUsersAsync();
            return Ok(users);
        }
        
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<string>> loginInfo([FromBody]User userinfo) {

            var users = await _usersRepository.LogingUser(userinfo.UserName,userinfo.Password);
            return Ok(users);


       }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> regInfo([FromBody] User userinfo) {
            var users = await _usersRepository.regUser(userinfo.UserName, userinfo.Password);
            return Ok(users);
        }
        
    }
}
