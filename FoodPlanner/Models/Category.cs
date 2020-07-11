using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Category()
        {

        }

        public Category(string name)
        {
            Name = name;
        }
    }
}
