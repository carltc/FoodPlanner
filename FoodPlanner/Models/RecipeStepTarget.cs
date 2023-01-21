using FoodPlanner.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class RecipeStepTarget
    {
        public int Id { get; set; }

        [NotMapped]
        public string Name
        {
            get
            {
                if (itemRecipeStepTarget != null) return itemRecipeStepTarget.Name;
                if (ingredientRecipeStepTarget != null) return ingredientRecipeStepTarget.Name;

                return null;
            }
        }

        [NotMapped]
        public string Category
        {
            get
            {
                if (itemRecipeStepTarget != null) return itemRecipeStepTarget.Category;
                if (ingredientRecipeStepTarget != null) return ingredientRecipeStepTarget.Category;

                return null;
            }
        }

        public RecipeStepTargetItem Item { get; set; }

        private IRecipeStepTarget itemRecipeStepTarget => (IRecipeStepTarget)Item;

        public Ingredient Ingredient { get; set; }

        private IRecipeStepTarget ingredientRecipeStepTarget => (IRecipeStepTarget)Ingredient;
    }
}
