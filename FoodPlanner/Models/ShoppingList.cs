using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public static class ShoppingList
    {
        public static int ShoppingListSize { get; set; }
        public static List<ShopItem> ShopItems = new List<ShopItem>();
        public static ShoppingListSortType SortType = ShoppingListSortType.Category;

        public static DateTime ExpectedShopDate { get; set; }

        internal static List<ShopItem> CopyMarkedItems(List<ShopItem> newShopItems, List<ShopItem> oldShopItems)
        {
            // Go through each item in the new list and see if it exists in the old list
            for (int i = 0; i < newShopItems.Count(); i++)
            {
                // get shop item
                var newShopItem = newShopItems[i];

                if (oldShopItems.Where(osi => osi.Product.Id == newShopItem.Product.Id).Count() > 0 && oldShopItems.Where(osi => osi.Product.Id == newShopItem.Product.Id).FirstOrDefault().Bought == true)
                {
                    newShopItems[i].Bought = true;
                }
            }

            return newShopItems;
        }

        internal static bool ResetShopItems()
        {
            try
            {
                for (int i = 0; i < ShopItems.Count(); i++)
                {
                    ShopItems[i].Bought = false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }



        //public ShoppingList(DateTime expectedShopDate, List<ShopItem> shopItems)
        //{
        //    ExpectedShopDate = expectedShopDate;
        //    ShopItems = shopItems;
        //}
    }

    public enum ShoppingListSortType
    {
        Category,
        FoodPlan
    }
}
