using FaithMiApplication1.Models;
using FaithMiApplication1.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FaithMiApplication1.Repositories;
using FaithMiApplication1.Repositories.Dao;
using FaithMiApplication1.Jwt;
using System;

namespace FaithMiApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderDao _orderRepository;
        private JwtSettings _jwtSettings;
        /// <summary>
        /// 构造方法
        /// </summary>
        public OrderController(IOrderDao orderDao)
        {
            _orderRepository = orderDao;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
         public async Task<ActionResult<ResMsgDTO>> AddOrder([FromBody]Shoppingcart shoppingcart) {
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
    }
}
