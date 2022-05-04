using FaithMiApplication1.Controllers;
using FaithMiApplication1.DTO;
using FaithMiApplication1.Models;
using FaithMiApplication1.Repositories.Dao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
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

       /// <summary>
       /// 添加购物车
       /// </summary>
       /// <param name="shoppingcart"></param>
       /// <returns></returns>
        public async Task<ResMsgDTO> AddOrder(Shoppingcart shoppingcart)
        {
            try
            {
                //判断是否已经拥有订单号
                var sc = await _faithdbContext.Shoppingcarts.FirstOrDefaultAsync(x => x.ProductId==shoppingcart.ProductId);
                if (sc!=null)
                {
                    sc.CartNum ++;
                    int addRow = _faithdbContext.SaveChanges();
                    if (addRow > 0)
                    {
                        return new ResMsgDTO
                        {
                            RegCode = 1,
                            RegMsg = "商品已经存在！数量+1！"
                        };
                    }
                    else
                    {
                        return new ResMsgDTO
                        {
                            RegCode = 0,
                            RegMsg = "暂时同一件商品只能加入同一件商品！"
                        };
                    }
                   
                }
                shoppingcart.CartNum = 1;
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

        /// <summary>
        /// 查询购物车列表
        /// </summary>
        /// <returns></returns>
       public ShoppingList GetShoppingcart(int userId)
        {
            MySqlParameter[] sqlparment = new MySqlParameter[] {
                new MySqlParameter("@userId", userId),

             };
            string sql = @"SELECT s.id,s.user_id,s.product_id,s.cart_num,p.product_price
                            FROM `shoppingcart` s
                            INNER JOIN product p
                            on s.product_id = p.product_id
                            where s.user_id=@userId
                            ";
            var list = _faithdbContext.Shoppingcarts.FromSqlRaw(sql, sqlparment).ToList();
            List<ShoppingDTO> shoppingDTO = new List<ShoppingDTO>();
            foreach (var item in list)
            {
                var pic = _faithdbContext.Products.Where(p => p.ProductId == item.ProductId).Include(s => s.ProductPictures).Select(s => new {
                    s.ProductPrice,
                    s.ProductPicture,
                    s.ProductName
                }).FirstOrDefault();
               shoppingDTO.Add(new ShoppingDTO
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    product_pic = pic.ProductPicture,
                    ProductId = item.ProductId,
                    CartNum = item.CartNum,
                    ProductPrice = pic.ProductPrice,
                    ProductName = pic.ProductName,
                    ProductTot=pic.ProductPrice* item.CartNum
                });
            }
            ShoppingList shoppingList = new ShoppingList();
            shoppingList.code = 0;
            shoppingList.data = shoppingDTO;
            shoppingList.count = shoppingDTO.Count;
            shoppingList.msg = "查询成功！";
            return shoppingList;
        }
        /// <summary>
        /// 根据购物车id 和userid删除
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ResMsgDTO DelShopping(DelShopDto delShopDto) {
            try
            {
                if (delShopDto.userId == null || delShopDto.sid == null)
                {
                    return new ResMsgDTO
                    {
                        RegCode = 1,
                        RegMsg = "参数不能为空"
                    };
                }
                else
                {
                    //进行删除操作
                    Shoppingcart cart =_faithdbContext.Shoppingcarts.Where(s=>s.Id== delShopDto.sid && s.UserId== delShopDto.userId).FirstOrDefault();
                    _faithdbContext.Shoppingcarts.Remove(cart);
                    _faithdbContext.SaveChanges();
                    return new ResMsgDTO
                    {
                        RegCode = 0,
                        RegMsg = "删除成功！"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResMsgDTO
                {
                    RegCode = 1,
                    RegMsg = ex.Message,
                };
            }
        }
        /// <summary>
        /// 获取购物车信息生成订单
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public OrderMsgDto AddOrderProduct(List<OrdersProduct> orders)
        {
            try
            {
                var oid = DateTime.Now.ToString("yyyyMMddHHmmss") + "12138";
                foreach (var item in orders)
                {
                    OrdersProduct od = new OrdersProduct();
                    //2022040368
                    od.OrderId = oid;
                    od.ProductId = item.ProductId;  
                    od.UserId = item.UserId;
                    od.BuyNum = item.BuyNum;
                    od.Money = item.Money;
                    
                    _faithdbContext.OrdersProducts.Add(od);
                }
               var row = _faithdbContext.SaveChanges();
                if (row>0)
                {
                    return new OrderMsgDto
                    {
                        RegCode = 0,
                        RegMsg = "生成订单成功！",
                        orderId=oid
                    };
                }
                else
                {
                    return new OrderMsgDto
                    {
                        RegCode = 1,
                        RegMsg = "生成订单失败！"
                    };
                }

            }
            catch (Exception ex)
            {
                return new OrderMsgDto
                {
                    RegCode = 1,
                    RegMsg = ex.Message
                };
            }
        }

        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Tuple<List<GetOrderProductDTO>, int> SelectOrderProduct(getOrderDto dto)
        {
            MySqlParameter[] sqlparment = new MySqlParameter[] {
                new MySqlParameter("@userid", dto.userId),
                new MySqlParameter("@order_id", dto.orderId)
             };
            string sql = @"SELECT o.*,p.product_name FROM `orders_product` o
                        INNER JOIN product p
                        on o.product_id=p.product_id
                        where order_id=@order_id and userid=@userid
                            ";
            var list = _faithdbContext.OrdersProducts.FromSqlRaw(sql, sqlparment).ToList();
            List<GetOrderProductDTO> getOrders = new List<GetOrderProductDTO>();
            var allProdSum = 0;
            foreach (var item in list)
            {
                var pic = _faithdbContext.Products.Where(p => p.ProductId == item.ProductId).Include(s => s.ProductPictures).Select(s => new {
                    s.ProductPrice,
                    s.ProductPicture,
                    s.ProductName
                }).FirstOrDefault();
                getOrders.Add(new GetOrderProductDTO
                {
                    prodPic=pic.ProductPicture,
                    prodName=pic.ProductName,
                    prodPrice=pic.ProductPrice,
                    prodNum=item.BuyNum,
                    prodSum= pic.ProductPrice* item.BuyNum

                });
                //累计总金额
                allProdSum += pic.ProductPrice * item.BuyNum;
                
            }
            return Tuple.Create(getOrders,allProdSum);
            
        }
        /// <summary>
        /// 结算订单
        /// </summary>
        /// <param name="endDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ResMsgDTO SettlementOrder(EndDto endDto)
        {
            try
            {
                //先生成支付订单
                string oid = "SO" + DateTime.Now.ToString("yyyyMMddHHmmss") + endDto.orderId;
                DateTime creatTime = DateTime.Now;
                var statusOrder = 1;
                var o = new Order();
                o.Id = oid;
                o.BuyTime = creatTime;
                o.UserId = long.Parse(endDto.userId);
                o.Status = statusOrder;
                _faithdbContext.Orders.Add(o);
                int rows = _faithdbContext.SaveChanges();
                if (rows > 0)
                {
                    return new ResMsgDTO
                    {
                        RegCode = 0,
                        RegMsg = "结算成功！"
                    };
                }
                else
                {
                    return new ResMsgDTO
                    {
                        RegCode = 1,
                        RegMsg = "结算失败！"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResMsgDTO
                {
                    RegCode = 1,
                    RegMsg = ex.Message
                };
            }
          
}

          
        
    }
}
