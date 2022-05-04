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
using System.Collections.Generic;
using FaithMiApplication1.DTO;

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
        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="ordersProduct"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
         public ActionResult<OrderMsgDto> AddOrderProduct([FromBody] List<OrdersProduct> ordersProduct) {
            try
            {
               
                return  _orderRepository.AddOrderProduct(ordersProduct);
            }
            catch (Exception ex)
            {
               return new OrderMsgDto
               { 
                RegCode=1,
                RegMsg=ex.Message
                };
            }
          
        }
        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="ordersProduct"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult<Tuple<List<GetOrderProductDTO>, int>> SelectOrderProduct([FromBody]getOrderDto dto)
        {
            try
            {

                return _orderRepository.SelectOrderProduct(dto);
            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// 结算订单
        /// </summary>
        /// <param name="ordersProduct"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult<ResMsgDTO> SettlementOrder([FromBody] EndDto dto)
        {
            try
            {

                return _orderRepository.SettlementOrder(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
