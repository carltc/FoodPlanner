using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class FoodPlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public ShopTrip ShopTrip { get; set; }
        public List<FoodPlanRecipe> Recipes { get; set; }
        public List<FoodPlanProduct> Products { get; set; }

        public FoodPlan()
        {

        }

        public FoodPlan(DateTime dateTime)
        {
            Name = "";
            Date = dateTime;
            Recipes = new List<FoodPlanRecipe>();
            Products = new List<FoodPlanProduct>();
        }
    }
}
