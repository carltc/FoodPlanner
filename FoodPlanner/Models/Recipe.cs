using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public int Portions { get; set; }
        public string AddedBy { get; set; } = "Unknown";
        public Cuisine Cuisine { get; set; }
    }
}
