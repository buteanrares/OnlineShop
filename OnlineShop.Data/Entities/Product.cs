namespace OnlineShop.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public bool Available { get; set; }
        public int? CartId { get; set; }
        public int? WishlistId { get; set; }
        public ICollection<CustomerProductCart>? CartCustomers { get; set; }
        public ICollection<CustomerProductWishlist>? WishlistCustomers { get; set; }
    }
}
