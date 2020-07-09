using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public static class ShoppingList
    {
        public static int ShoppingListSize { get; set; }
        public static List<ShopItem> ShopItems { get; set; }

        public static DateTime ExpectedShopDate { get; set; }

        //public ShoppingList(DateTime expectedShopDate, List<ShopItem> shopItems)
        //{
        //    ExpectedShopDate = expectedShopDate;
        //    ShopItems = shopItems;
        //}
    }
}
