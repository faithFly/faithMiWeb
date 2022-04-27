using FaithMiApplication1.Models;
using FaithMiApplication1.Repositories.Dao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaithMiApplication1.Repositories.Impl
{
    public class ShoppingDao : IOrderDao
    {
        private readonly faithdbContext _faithdbContext;
        public ShoppingDao(faithdbContext faithdb)
        {
            _faithdbContext = faithdb ?? throw new ArgumentNullException(nameof(faithdb));
        }

       
        public async Task<ResMsgDTO> AddOrder(Shoppingcart shoppingcart)
        {
            try
            {
                //判断是否已经拥有订单号
                Shoppingcart sc = await _faithdbContext.Shoppingcarts.FirstOrDefaultAsync(x => x.Id == shoppingcart.Id);
                if (sc!=null)
                {
                    return new ResMsgDTO
                    {
                        RegCode = 1,
                        RegMsg = "订单号重复有错误！"
                    };
                }
                _faithdbContext.Shoppingcarts.Add(shoppingcart);
                int row=_faithdbContext.SaveChanges();
                if (row>0)
                {
                    return new ResMsgDTO
                    {
                        RegCode = 1,
                        RegMsg = "加入购物车成功！"
                    };
                }
                else
                {
                    return new ResMsgDTO
                    {
                        RegCode = 0,
                        RegMsg = "加入购物车失败！"
                    };
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
