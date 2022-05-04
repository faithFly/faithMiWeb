using FaithMiApplication1.Models;
using FaithMiApplication1.Repositories.Dao;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FaithMiApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly IOrderDao _orderRepository;
        //private JwtSettings _jwtSettings;
        /// <summary>
        /// 构造方法
        /// </summary>
        public ShoppingController(IOrderDao orderDao)
        {
            _orderRepository = orderDao;
        }
        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="shoppingcart"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<ActionResult<ResMsgDTO>> AddOrder([FromBody] Shoppingcart shoppingcart)
        {
            try
            {
                var shop = _orderRepository.AddOrder(shoppingcart);
                return await shop;
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// 查询购物车
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ShoppingList> GetShoppingcart(int userId)
        {
            try
            {
                
                return _orderRepository.GetShoppingcart(userId);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// 删除购物车
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult<ResMsgDTO> DelShopping([FromBody] DelShopDto delShopDto) {
            try
            {
                return _orderRepository.DelShopping(delShopDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
