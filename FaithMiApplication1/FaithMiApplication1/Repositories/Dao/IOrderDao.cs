using FaithMiApplication1.Controllers;
using FaithMiApplication1.DTO;
using FaithMiApplication1.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaithMiApplication1.Repositories.Dao
{
    public interface IOrderDao
    {
        // 加入购物车
        Task<ResMsgDTO> AddOrder(Shoppingcart shoppingcart);

        //显示购物车
        ShoppingList GetShoppingcart(int userId);
        //购物车删除
        ResMsgDTO DelShopping(DelShopDto delShopDto);

        //生成订单
        OrderMsgDto AddOrderProduct(List<OrdersProduct> orders);
        //查询订单明细
        Tuple<List<GetOrderProductDTO>, int> SelectOrderProduct(getOrderDto dto);
        //结算订单
        ResMsgDTO SettlementOrder(EndDto endDto);
    }
}
