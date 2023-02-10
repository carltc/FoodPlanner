using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class RecipeStepIngredient
    {
        public int RecipeStepId { get; set; }
        public RecipeStep RecipeStep { get; set; }

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        public override string ToString()
        {
            return $"RS:{RecipeStep?.Id} - I:{Ingredient?.Id}";
        }
    }
}
