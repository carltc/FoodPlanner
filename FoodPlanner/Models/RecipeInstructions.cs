using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class RecipeInstructions
    {
        public int Id { get; set; }

        public List<RecipeStep> Steps { get; set; } = new List<RecipeStep>();
    }
}
