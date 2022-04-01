using System;
using System.Collections.Generic;

#nullable disable

namespace FaithMiApplication1.Models
{
    public partial class OrdersProduct
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? BuyNum { get; set; }
        public decimal? Money { get; set; }
    }
}
