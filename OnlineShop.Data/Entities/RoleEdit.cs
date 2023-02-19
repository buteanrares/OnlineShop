using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.Entities
{
    public class RoleEdit
    {

        public IdentityRole Role { get; set; }
        public IEnumerable<UserAccount> Members { get; set; }
        public IEnumerable<UserAccount> NonMembers { get; set; }

    }
}
