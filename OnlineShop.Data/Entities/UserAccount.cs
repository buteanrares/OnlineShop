
using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Data.Entities
{
    public class UserAccount : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
