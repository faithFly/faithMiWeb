using FaithMiApplication1.Models;
using FaithMiApplication1.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
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
        /// <summary>
        /// 构造方法
        /// </summary>
        public listMainController(IProductDao productDao)
        {
            _productDao = productDao;
        }
        /// <summary>
        /// 获取商品分类
        /// </summary>
        /// <returns></returns>
        ///[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            var cates = await _productDao.GetCategory();
            return Ok(cates);
        }
        /// <summary>
        /// 通过商品分类获取商品列表并取得前9个
        /// </summary>
        /// <param name="cateName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCateName(string cateName)
        {
            var prod = await _productDao.GetProductByCateName(cateName);
            return Ok(prod);
         }
    }
}
