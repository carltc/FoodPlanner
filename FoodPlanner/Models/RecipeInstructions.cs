using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class RecipeInstructions
    {
        public int Id { get; set; }

        public List<RecipeStep> Steps { get; set; } = new List<RecipeStep>();
    }
}
