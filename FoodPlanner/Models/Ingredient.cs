﻿using FoodPlanner.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class Ingredient : ShopItem
    {
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
