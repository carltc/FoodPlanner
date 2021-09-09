using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FoodPlanner.Data
{
    public class FoodPlannerContext : IdentityDbContext<AppUser>
    {
        public FoodPlannerContext (DbContextOptions<FoodPlannerContext> options)
            : base(options)
        {
        }

        public DbSet<FoodPlanner.Models.Ingredient> Ingredients { get; set; }

        public DbSet<FoodPlanner.Models.Recipe> Recipes { get; set; }

        public DbSet<FoodPlanner.Models.ProductType> ProductTypes { get; set; }

        public DbSet<FoodPlanner.Models.Category> Categorys { get; set; }

        public DbSet<FoodPlanner.Models.Product> Products { get; set; }

        public DbSet<FoodPlanner.Models.FoodPlan> FoodPlans { get; set; }

        public DbSet<FoodPlanner.Models.ShopItem> ShopItems { get; set; }

        public DbSet<FoodPlanner.Models.FoodPlanRecipe> FoodPlanRecipes { get; set; }

        public DbSet<FoodPlanner.Models.ShopTrip> ShopTrips { get; set; }

        public DbSet<FoodPlanner.Models.Household> Households { get; set; }

        public DbSet<FoodPlanner.Models.HouseholdUser> HouseholdUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HouseholdUser>().HasKey(sc => new { sc.HouseholdId, sc.AppUserId });

            modelBuilder.Entity<HouseholdUser>()
                .HasOne<Household>(sc => sc.Household)
                .WithMany(s => s.HouseholdUsers)
                .HasForeignKey(sc => sc.HouseholdId);


            modelBuilder.Entity<HouseholdUser>()
                .HasOne<AppUser>(sc => sc.AppUser)
                .WithMany(s => s.HouseholdUsers)
                .HasForeignKey(sc => sc.AppUserId);
        }
    }
}
