using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class ShoppingList
    {
        public List<ShopItem> ShopItems { get; set; }

        public DateTime ExpectedShopDate { get; set; }

        public ShoppingList(DateTime expectedShopDate, List<ShopItem> shopItems)
        {
            ExpectedShopDate = expectedShopDate;
            ShopItems = shopItems;
        }
    }
}
