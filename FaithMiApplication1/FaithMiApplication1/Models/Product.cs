using System;
using System.Collections.Generic;

#nullable disable

namespace FaithMiApplication1.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductPictures = new HashSet<ProductPicture>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductIntro { get; set; }
        public string ProductPicture { get; set; }
        public int ProductPrice { get; set; }
        public double ProductSellingPrice { get; set; }
        public int ProductNum { get; set; }
        public int ProductSales { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<ProductPicture> ProductPictures { get; set; }
    }
}
