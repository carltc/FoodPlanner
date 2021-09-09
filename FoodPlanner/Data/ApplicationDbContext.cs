using System;
using System.Collections.Generic;
using System.Text;
using FoodPlanner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodPlanner.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FoodPlanner.Models.Household> Household { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-FoodPlanner-22A3D535-0157-403D-BB7F-5D3BE4489561;Trusted_Connection=True;MultipleActiveResultSets=true");
        //}
    }
}
