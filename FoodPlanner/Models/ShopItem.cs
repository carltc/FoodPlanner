using FoodPlanner.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class ShopItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public double Quantity { get; set; }
        public Unit Unit { get; set; }

        [NotMapped]
        public bool Bought = false;
    }
}
