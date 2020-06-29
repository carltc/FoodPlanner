using FoodPlanner.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public Unit Unit { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
