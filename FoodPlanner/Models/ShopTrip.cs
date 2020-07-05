using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class ShopTrip
    {
        public int Id { get; set; }
        public DateTime ShopTripCompleted { get; set; }
        public int FoodPlanId { get; set; }
        public FoodPlan FoodPlan { get; set; }
    }
}
