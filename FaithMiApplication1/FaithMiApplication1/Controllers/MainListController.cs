using FaithMiApplication1.Models;
using FaithMiApplication1.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FaithMiApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainListController : ControllerBase
    {
        private readonly IProductDao _productDao;
        /// <summary>
        /// 构造方法
        /// </summary>
        public MainListController(IProductDao productDao)
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
        [HttpPost]
        public async Task<HttpResponseMessage> GetProductByCateName([FromBody] string cateName)
        {
          
            var prod = await _productDao.GetProductByCateName(cateName);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(prod);
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(json, Encoding.GetEncoding("UTF-8"), "application/json") };
            return  result;
            
        }
    }
}
