﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class FoodPlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<FoodPlanRecipe> Recipes { get; set; }
        public List<FoodPlanProduct> Products { get; set; }
    }
}
