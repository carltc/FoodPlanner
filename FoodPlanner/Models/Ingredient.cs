using FoodPlanner.Classes;
using FoodPlanner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class Ingredient : ShopItem, IRecipeStepTarget
    {
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public string Category => Product.Category?.Name;

        public Ingredient()
        {

        }

        public Ingredient(string name, float quantity, string unit, Product product)
        {
            Name = name;
            Product = product;
            Quantity = quantity;
            if (unit == null)
            {
                Unit = Unit;
            }
            else
            {
                Unit = MeasurementUnit.ConvertToUnit(unit);
            }
        }

        public Ingredient(Ingredient ingredient)
        {
            Id = ingredient.Id;
            Name = ingredient.Name;
            ProductId = ingredient.ProductId;
            Product = ingredient.Product;
            Quantity = ingredient.Quantity;
            Unit = ingredient.Unit;
            Bought = ingredient.Bought;
            RecipeId = ingredient.RecipeId;
            Recipe = ingredient.Recipe;
        }
    }
}
