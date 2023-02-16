namespace OnlineShop.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public Customer? Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ETA { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
