using System.Collections.Generic;

namespace FaithMiApplication1.Models
{
    public class ShoppingList
    {
        public int code { get; set; }
        public string msg { get; set; }
        public int count { get; set; }
        public List<ShoppingDTO> data { get; set; }
    }
}
