using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shisha.Models.Entities
{
    public class UserRole : IdentityUserRole<int> // nu mai trb sa punem role si user id ca deja le are identity user role
    {
        public Role Role { get; set; }
        public User User { get; set; }
    }
}
