using FaithMiApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace FaithMiApplication1.Repositories
{
    public class ProductDao : IProductDao
    {
        private readonly faithdbContext _faithdbContext;
        public ProductDao(faithdbContext faithdb)
        {
            _faithdbContext = faithdb ?? throw new ArgumentNullException(nameof(faithdb));
        }
        /// <summary>
        /// 获取商品分类
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Category>> GetCategory()
        {
            return await _faithdbContext.Categories.ToArrayAsync();

        }

        /// <summary>
        /// 根据商品分类名称获取分类id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<Category>> GetCategoryId(string name)
        {
            return await _faithdbContext.Categories.Where(c => c.CategoryName == name).ToArrayAsync();
        }
        /// <summary>
        /// 通过商品分类获取商品列表并取得前9个
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<Product>> GetProductByCateName(string name)
        {

            if (name == "全部")
            {
                return await _faithdbContext.Products.ToArrayAsync();
            }
            MySqlParameter[] sqlparment = new MySqlParameter[] { new MySqlParameter("@TestName", name) };
            FormattableString fstr = $@"SELECT p.product_id,p.product_title,p.product_intro,p.product_picture,p.product_price,
                                       c.category_id,p.product_name,p.product_num,p.product_selling_price,p.product_sales
                                        from product p 
                                        LEFT JOIN 
                                        category c 
                                        ON p.category_id =c.category_id 
                                        WHERE c.category_name=@TestName
                                        LIMIT 0,9";
            return await _faithdbContext.Products.FromSqlRaw(fstr.ToString(), sqlparment).ToListAsync();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Tuple<List<Product>, int>> getProductByPage(getProdPage page)
        {
            //分页查询
            if (page.name == "全部")
            {
                var allList = _faithdbContext.Products.OrderBy(p => p.ProductId).Skip(page.pageSize * (page.pageNum - 1)).Take(page.pageSize).ToList();
                int index = _faithdbContext.Products.ToList().Count;
                return Tuple.Create(allList, index);
            }
            else { 
            MySqlParameter[] sqlparment = new MySqlParameter[] {
                new MySqlParameter("@TestName", page.name),
                new MySqlParameter("@pageStr", (page.pageNum-1)*page.pageSize),
                new MySqlParameter("@pageLimit",page.pageSize),
             };
            FormattableString fstr = $@"SELECT p.product_id,p.product_title,p.product_intro,p.product_picture,p.product_price,
                                      c.category_id,p.product_name,p.product_num,p.product_selling_price,p.product_sales
                                    from product p 
                                    LEFT JOIN 
                                    category c 
                                    ON p.category_id =c.category_id 
                                    WHERE c.category_name=@TestName
                                    LIMIT @pageStr,@pageLimit";
            var list = await _faithdbContext.Products.FromSqlRaw(fstr.ToString(), sqlparment).ToListAsync();
            int num = list.Count;
            var selectCount = _faithdbContext.Products.Include(c => c.Category).Where(c => c.Category.CategoryName == page.name).Count();
            return Tuple.Create(list, selectCount);
        }

        }

        /// <summary>
        /// 通过商品id获取商品图片和商品参数
        /// </summary>
        /// <param name="prodId"></param>
        /// <returns></returns>
        public async Task<Tuple<List<Product>, List<ProductPicture>>> GetProductByProdId(int prodId)
        {
            var prod_List=await _faithdbContext.Products.Where(p=>p.ProductId == prodId).ToListAsync();
            var prod_Picture=await _faithdbContext.ProductPictures.Where(p=>p.ProductId==prodId).ToListAsync();
            return Tuple.Create(prod_List, prod_Picture);
        }
    }
}
