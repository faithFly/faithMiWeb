using System.Collections.Generic;
using System.Threading.Tasks;
using FaithMiApplication1.Models;
namespace FaithMiApplication1.Repositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        //用户简易登录
        Task<LoginMsgDTO> LogingUser(string name,string pwd);

        //用户注册
        Task<ResMsgDTO> regUser(string name,string pwd);

       
    }
}
