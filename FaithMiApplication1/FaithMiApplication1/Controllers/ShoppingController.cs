using FaithMiApplication1.Models;
using FaithMiApplication1.Repositories.Dao;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
    }
}
