namespace OnlineShop.Data.Entities
{
    internal class Employee : UserAccount
    {
        public DateTime HireDate { get; set; }
        public int Salary { get; set; }
    }
}
