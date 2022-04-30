using FaithMiApplication1.Models;
using FaithMiApplication1.Repositories;
using FaithMiApplication1.Repositories.Dao;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FaithMiApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class listMainController : ControllerBase
    {
        private readonly IProductDao _productDao;
        private readonly IOrderDao _orderRepository;
        /// <summary>
        /// 构造方法
        /// </summary>
        public listMainController(IProductDao productDao, IOrderDao orderDao)
        {
            _productDao = productDao;
            _orderRepository = orderDao;
        }
        /// <summary>
        /// 获取商品分类
        /// </summary>
        /// <returns></returns>
        ///[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            try
            {
                var cates = await _productDao.GetCategory();
                return Ok(cates);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        /// <summary>
        /// 通过商品分类获取商品列表并取得前9个
        /// </summary>
        /// <param name="cateName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCateName(string cateName)
        {
            try
            {
                var prod = await _productDao.GetProductByCateName(cateName);
                return Ok(prod);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
         }
        /// <summary>
        /// 获取行数
        /// </summary>
        /// <param name="cateName"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Tuple<List<Product>, int>>> GetProductPageNum([FromBody]getProdPage page)
        {
            try
            {
                return await _productDao.getProductByPage(page);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [HttpGet]
        public async Task<ActionResult<Tuple<List<Product>, List<ProductPicture>>>> GetProductByProdId(int prodId) {
            try
            {
                Tuple<List<Product>, List<ProductPicture>> dbcon = await _productDao.GetProductByProdId(prodId);
                return Ok(dbcon);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
      
    }
}
