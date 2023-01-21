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

        public string PreText { get; set; }

        public RecipeStepAction Action { get; set; }

        public string MidText { get; set; }

        public RecipeStepTarget Target { get; set; }

        public string PostText { get; set; }

    }
}
