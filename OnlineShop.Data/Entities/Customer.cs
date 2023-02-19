namespace OnlineShop.Data.Entities
{
    public class Customer : UserAccount
    {
        public ICollection<Order>? Orders { get; set; }
        public ICollection<CustomerProductCart>? CartProducts { get; set; }
        public ICollection<CustomerProductWishlist>? WishlistProducts { get; set; }
    }
}
