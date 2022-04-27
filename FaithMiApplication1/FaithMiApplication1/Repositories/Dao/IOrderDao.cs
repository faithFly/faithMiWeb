using FaithMiApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaithMiApplication1.Repositories.Dao
{
    public interface IOrderDao
    {
        // 加入购物车
        Task<ResMsgDTO> AddOrder(Shoppingcart shoppingcart);
    }
}
