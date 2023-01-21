using FoodPlanner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    /// <summary>
    /// This class defines an item that can be targeted by a Recipe Step (such as 'Oven', 'Chopping Board' etc.)
    /// </summary>
    public class RecipeStepTargetItem : IRecipeStepTarget
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }
    }
}
