namespace OnlineShop.Data.Entities
{
    public class Employee : UserAccount
    {
        public DateTime HireDate { get; set; }
        public int Salary { get; set; }
    }
}
