namespace FaithMiApplication1.Models
{
    public class ShoppingDTO
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public int CartNum { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string product_pic { get; set; }
        public int ProductTot { get; set; }
    }
}
