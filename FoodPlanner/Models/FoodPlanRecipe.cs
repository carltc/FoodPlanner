using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class FoodPlanRecipe : FoodPlanItem
    {
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
