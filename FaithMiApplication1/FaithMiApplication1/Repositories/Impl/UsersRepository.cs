using FaithMiApplication1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FaithMiApplication1.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly faithdbContext _faithdbContext;
        public UsersRepository(faithdbContext faithdb)
        {
            _faithdbContext = faithdb ?? throw new ArgumentNullException(nameof(faithdb));
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _faithdbContext.Users.ToArrayAsync();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public async Task<LoginMsgDTO> LogingUser(string name, string pwd)
        {
            LoginMsgDTO loginMsgDTO = new LoginMsgDTO();
            try
            {
                
               
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(pwd))
                {
                    loginMsgDTO.LoginCode = 0;
                    loginMsgDTO.LoginMsg = "账号或密码为空";
                    return loginMsgDTO;
                }
                User user = await _faithdbContext.Users.FirstOrDefaultAsync(x => x.UserName == name);
                if (user == null)
                {
                    loginMsgDTO.LoginCode = 0;
                    loginMsgDTO.LoginMsg = "用户不存在";
                    return loginMsgDTO;
                }
                else
                {
                    string res3 = "";
                    using (var md5 = MD5.Create())
                    {
                        var result = md5.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                        var strResult = BitConverter.ToString(result);
                        res3 = strResult.Replace("-", "");

                    }
                    User user2 = await _faithdbContext.Users.FirstOrDefaultAsync(x => x.UserName == name && x.Password == res3);
                    if (user2 == null)
                    {
                        loginMsgDTO.LoginCode = 0;
                        loginMsgDTO.LoginMsg = "账号或密码错误";
                        return loginMsgDTO;
                    }
                    else
                    {
                        //登录成功返回id
                        loginMsgDTO.LoginCode = 1;
                        loginMsgDTO.LoginMsg = "登录成功！";
                        loginMsgDTO.UserId= user2.UserId;
                        loginMsgDTO.UserName= user2.UserName;
                        return loginMsgDTO;
                    }
                }
            }
            catch (Exception ex)
            {
                loginMsgDTO.LoginCode = 0;
                loginMsgDTO.LoginMsg = ex.ToString();
                return loginMsgDTO;
            }




        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResMsgDTO> regUser(string name, string pwd)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(name)||string.IsNullOrWhiteSpace(pwd))
                {
                    return new ResMsgDTO
                    {
                        RegCode = 0,
                        RegMsg="输入内容有为空"
                    };
                }
                else
                {
                    //判断用户是否存在
                    User user = await _faithdbContext.Users.FirstOrDefaultAsync(x => x.UserName == name);
                    if (user == null)
                    {
                        //用户不存在可以注册
                        //将密码MD5加密
                        string res3="";
                        using (var md5 = MD5.Create())
                        {
                            var result = md5.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                            var strResult = BitConverter.ToString(result);
                            res3 = strResult.Replace("-", "");
                            
                        }
                        _faithdbContext.Users.Add(new User
                        {
                            UserName = name,
                            Password = res3
                        });
                        int row = _faithdbContext.SaveChanges();
                        if (row > 0)
                        {
                            return new ResMsgDTO
                            {
                                RegCode = 1,
                                RegMsg = "注册成功！"
                            };
                        }
                        else
                        {
                            return new ResMsgDTO
                            {
                                RegCode = 0,
                                RegMsg = "注册失败！"
                            };
                        }
                    }
                    else
                    {
                        return new ResMsgDTO
                        {
                            RegCode = 0,
                            RegMsg = "用户已存在！"
                        };
                    }

                }
            }
            catch (Exception ex)
            {

                return new ResMsgDTO
                {
                    RegCode = 0,
                    RegMsg = ex.Message
                }; 
            }

        }
    }
}
