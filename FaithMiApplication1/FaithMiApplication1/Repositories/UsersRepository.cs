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
        public async Task<Dictionary<int, string>> LogingUser(string name, string pwd)
        {
            Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();
            try
            {
               
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(pwd))
                {
                    keyValuePairs.Add(0, "用户名密码不能为空！");
                    return keyValuePairs;
                }
                User user = await _faithdbContext.Users.FirstOrDefaultAsync(x => x.UserName == name);
                if (user == null)
                {
                    keyValuePairs.Add(0, "用户不存在！");
                    return keyValuePairs;
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
                        keyValuePairs.Add(0, "账号密码不正确！");
                        return keyValuePairs;
                    }
                    else
                    {
                        //登录成功返回id
                        keyValuePairs.Add(user2.UserId, "登录成功！");
                        return keyValuePairs;
                    }
                }
            }
            catch (Exception ex)
            {
               keyValuePairs.Add(0, ex.ToString());
                return keyValuePairs;
            }




        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> regUser(string name, string pwd)
        {
            try
            {
                if (name == string.Empty || pwd == string.Empty || name == null || pwd == null)
                {
                    return "用户名密码不能为空！";
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
                            return "注册成功！";
                        }
                        else
                        {
                            return "注册失败！";
                        }
                    }
                    else
                    {
                        return "用户已经存在，请更换一个用户名！";
                    }

                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }
    }
}
