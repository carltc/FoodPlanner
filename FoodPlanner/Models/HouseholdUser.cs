using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class HouseholdUser
    {
        public int HouseholdId { get; set; }
        public Household Household { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
