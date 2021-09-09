using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FoodPlanner.Models
{
    public class AppUser : IdentityUser
    {
        public List<HouseholdUser> HouseholdUsers { get; set; }
    }
}
