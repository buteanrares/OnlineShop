namespace OnlineShop.Data.Entities
{
    internal class Customer : UserAccount
    {
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Product>? WishlistProducts { get; set; }
    }
}
