using System;
using System.Collections.Generic;

#nullable disable

namespace FaithMiApplication1.Models
{
    public partial class Order
    {
        public string Id { get; set; }
        public long? UserId { get; set; }
        public DateTime? BuyTime { get; set; }
        public int? Status { get; set; }
    }
}
