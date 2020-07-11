using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ProductType()
        {

        }

        public ProductType(string name)
        {
            Name = name;
        }

    }
}
