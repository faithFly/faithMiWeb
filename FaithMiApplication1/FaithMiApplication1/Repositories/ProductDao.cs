using FaithMiApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;

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
            if (name=="全部") {
                return await _faithdbContext.Products.Where(s=>s.ProductId<=12).ToArrayAsync();
            }
            MySqlParameter[] sqlparment = new MySqlParameter[] { new MySqlParameter("@TestName", name) };
            FormattableString fstr = $@"SELECT p.product_id,p.product_title,p.product_intro,p.product_picture,p.product_price,c.category_id,p.product_name,p.product_num,p.product_selling_price,p.product_sales
                            from product p 
                            LEFT JOIN 
                            category c 
                            ON p.category_id =c.category_id 
                            WHERE c.category_name=@TestName
                            LIMIT 0,9";
             return await _faithdbContext.Products.FromSqlRaw(fstr.ToString(), sqlparment).ToListAsync();
        }

       
    }
}
