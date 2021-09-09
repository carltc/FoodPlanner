﻿// <auto-generated />
using System;
using FoodPlanner.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoodPlanner.Migrations
{
    [DbContext(typeof(FoodPlannerContext))]
    [Migration("20200823163536_AddHouseholds")]
    partial class AddHouseholds
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FoodPlanner.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("FoodPlanner.Models.FoodPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FoodPlan");
                });

            modelBuilder.Entity("FoodPlanner.Models.FoodPlanRecipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FoodPlanId")
                        .HasColumnType("int");

                    b.Property<int>("Meal")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FoodPlanId");

                    b.HasIndex("RecipeId");

                    b.ToTable("FoodPlanRecipe");
                });

            modelBuilder.Entity("FoodPlanner.Models.Household", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Household");
                });

            modelBuilder.Entity("FoodPlanner.Models.HouseholdUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HouseholdId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HouseholdId");

                    b.ToTable("HouseholdUser");
                });

            modelBuilder.Entity("FoodPlanner.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("FoodPlanner.Models.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductType");
                });

            modelBuilder.Entity("FoodPlanner.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Portions")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Recipe");
                });

            modelBuilder.Entity("FoodPlanner.Models.ShopItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ShopItem");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ShopItem");
                });

            modelBuilder.Entity("FoodPlanner.Models.ShopTrip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FoodPlanId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ShopTripCompleted")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("FoodPlanId")
                        .IsUnique();

                    b.ToTable("ShopTrip");
                });

            modelBuilder.Entity("FoodPlanner.Models.FoodPlanProduct", b =>
                {
                    b.HasBaseType("FoodPlanner.Models.ShopItem");

                    b.Property<int>("FoodPlanId")
                        .HasColumnType("int");

                    b.Property<int>("Meal")
                        .HasColumnType("int");

                    b.HasIndex("FoodPlanId");

                    b.HasDiscriminator().HasValue("FoodPlanProduct");
                });

            modelBuilder.Entity("FoodPlanner.Models.Ingredient", b =>
                {
                    b.HasBaseType("FoodPlanner.Models.ShopItem");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasIndex("RecipeId");

                    b.HasDiscriminator().HasValue("Ingredient");
                });

            modelBuilder.Entity("FoodPlanner.Models.FoodPlanRecipe", b =>
                {
                    b.HasOne("FoodPlanner.Models.FoodPlan", "FoodPlan")
                        .WithMany("Recipes")
                        .HasForeignKey("FoodPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodPlanner.Models.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodPlanner.Models.HouseholdUser", b =>
                {
                    b.HasOne("FoodPlanner.Models.Household", "Household")
                        .WithMany("Users")
                        .HasForeignKey("HouseholdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodPlanner.Models.Product", b =>
                {
                    b.HasOne("FoodPlanner.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("FoodPlanner.Models.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodPlanner.Models.ShopItem", b =>
                {
                    b.HasOne("FoodPlanner.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodPlanner.Models.ShopTrip", b =>
                {
                    b.HasOne("FoodPlanner.Models.FoodPlan", "FoodPlan")
                        .WithOne("ShopTrip")
                        .HasForeignKey("FoodPlanner.Models.ShopTrip", "FoodPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodPlanner.Models.FoodPlanProduct", b =>
                {
                    b.HasOne("FoodPlanner.Models.FoodPlan", "FoodPlan")
                        .WithMany("Products")
                        .HasForeignKey("FoodPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodPlanner.Models.Ingredient", b =>
                {
                    b.HasOne("FoodPlanner.Models.Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
