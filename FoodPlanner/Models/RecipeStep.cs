using FoodPlanner.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class RecipeStep
    {
        public int Id { get; set; }

        public int Order { get; set; }

        public string Text { get; set; }

        [NotMapped]
        public List<Ingredient> Ingredients => RecipeStepIngredients?.Select(rsi => rsi.Ingredient).ToList();
        public List<RecipeStepIngredient> RecipeStepIngredients { get; set; } = new List<RecipeStepIngredient>();

        /// <summary>
        /// Clear and re-populate the list of ingredients from the step Text and the given complete list of recipe ingredients
        /// </summary>
        /// <param name="recipeIngredients">A complete list of all ingredients used in the recipe this step is from</param>
        internal void PopulateIngredients(List<Ingredient> recipeIngredients)
        {
            var previousRecipeStepIngredients = RecipeStepIngredients;
            RecipeStepIngredients.Clear();
            MatchCollection matches = Regex.Matches(Text, @"""([\w|\s]+)""");

            foreach(Match match in matches)
            {
                var ingredient = recipeIngredients.Find(r => r.Name == match.Groups[1].ToString());
                // Check that this ingredient hasn't already been added to this step, and only add if it has not
                if (ingredient != null && !RecipeStepIngredients.Any(rsi => rsi.Ingredient.Id == ingredient.Id))
                {
                    // Check if this recipe step ingredient already existed and if so then just use that one
                    var existingRecipeStepIngredient = previousRecipeStepIngredients.Find(rsi => rsi.IngredientId == ingredient.Id && rsi.RecipeStepId == this.Id);
                    if (existingRecipeStepIngredient != null)
                    {
                        RecipeStepIngredients.Add(existingRecipeStepIngredient);
                    }
                    else
                    {
                        var recipeStepIngredient = new RecipeStepIngredient()
                        {
                            RecipeStep = this,
                            Ingredient = ingredient
                        };

                        RecipeStepIngredients.Add(recipeStepIngredient);
                    }
                }
            }
        }
    }
}
