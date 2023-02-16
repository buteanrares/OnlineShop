namespace OnlineShop.Data.Entities
{
    public class Customer : UserAccount
    {
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Product>? WishlistProducts { get; set; }
    }
}
