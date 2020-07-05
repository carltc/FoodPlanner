using FoodPlanner.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class FoodPlanProduct : ShopItem
    {
        public int FoodPlanId { get; set; }
        public FoodPlan FoodPlan { get; set; }
        public Meal Meal { get; set; }
    }
}
