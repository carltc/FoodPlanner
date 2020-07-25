﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class RecipeResults
    {
        public List<OnlineRecipe> results { get; set; }
        public int offset { get; set; }
        public int number { get; set; }
        public int totalResults { get; set; }
    }

    public class OnlineRecipe
    {
        public int id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string imagePath
        {
            get { return "https://spoonacular.com/recipeImages/" + image; }
            set { image = value; }
        }
        public string imageType { get; set; }
        public int readyInMinutes { get; set; }
        public string summary { get; set; }
        public List<OnlineIngredient> extendedIngredients { get; set; }
    }

    public class OnlineIngredient
    {
        public int id { get; set; }
        public string aisle { get; set; }
        public string name { get; set; }
        public float amount { get; set; }
        public string unit { get; set; }
    }
}
