using FoodPlanner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class RecipeStep
    {
        public int Id { get; set; }

        public int Order { get; set; }

        public string Text { get; set; }

    }
}
