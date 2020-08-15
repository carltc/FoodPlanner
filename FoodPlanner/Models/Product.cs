using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        public Product()
        {

        }
        public Product(string name, string category, string productType)
        {
            Name = name;
            Category = new Category(category);
            ProductType = new ProductType(productType);
        }
    }
}
