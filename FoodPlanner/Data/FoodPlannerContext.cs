using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Models;

namespace FoodPlanner.Data
{
    public class FoodPlannerContext : DbContext
    {
        public FoodPlannerContext (DbContextOptions<FoodPlannerContext> options)
            : base(options)
        {
        }

        public DbSet<FoodPlanner.Models.Ingredient> Ingredient { get; set; }

        public DbSet<FoodPlanner.Models.Recipe> Recipe { get; set; }

        public DbSet<FoodPlanner.Models.Consumable> Consumable { get; set; }
    }
}
