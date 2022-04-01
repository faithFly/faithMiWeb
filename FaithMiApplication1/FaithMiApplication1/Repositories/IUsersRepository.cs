using System.Collections.Generic;
using System.Threading.Tasks;
using FaithMiApplication1.Models;
namespace FaithMiApplication1.Repositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        //用户简易登录
        Task<Dictionary<int,string>> LogingUser(string name,string pwd);

        //用户注册
        Task<string> regUser(string name,string pwd);

       
    }
}
