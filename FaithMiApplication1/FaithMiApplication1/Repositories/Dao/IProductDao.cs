using FaithMiApplication1.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaithMiApplication1.Repositories
{
    public interface IProductDao
    {
        // 连接数据库根据商品分类名称获取分类id
        Task<IEnumerable<Category>> GetCategoryId(string name);

        //连接数据库获取商品分类
        Task<IEnumerable<Category>> GetCategory();

        //通过商品分类获取商品列表并取得前9个
        Task<IEnumerable<Product>> GetProductByCateName(string name);
        
        //分页查询
        Task<Tuple<List<Product>, int>> getProductByPage(getProdPage page);

        //根据productid查询商品参数
        Task<Tuple<List<Product>,List<ProductPicture>>> GetProductByProdId(int prodId);


    }
}
