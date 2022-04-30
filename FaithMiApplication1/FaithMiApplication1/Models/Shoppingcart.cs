using System;
using System.Collections.Generic;

#nullable disable

namespace FaithMiApplication1.Models
{
    public partial class Shoppingcart
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
    }
}
