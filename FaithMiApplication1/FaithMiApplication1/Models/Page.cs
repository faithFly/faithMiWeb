using System.ComponentModel;

namespace FaithMiApplication1.Models
{
    public class getProdPage
    {
        
        /// <summary>
        /// 商品分类名称
        /// </summary>
        [DisplayName("商品分类名称")]
        public string name { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        [DisplayName("当前页数")]
        public int pageNum { get; set; }
        /// <summary>
        /// 每页总行
        /// </summary>
        [DisplayName("每页总行")]
        public int pageSize { get; set; }
    }
}
