using FoodPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Classes
{
    public static class MeasurementUnit
    {
        public static ShopItem ConvertToStandardUnits(Ingredient ingredient)
        {
            return ConvertToStandardUnits((ShopItem)ingredient);
        }
        public static ShopItem ConvertToStandardUnits(FoodPlanProduct foodPlanProduct)
        {
            return ConvertToStandardUnits((ShopItem)foodPlanProduct);
        }

        public static ShopItem ConvertToStandardUnits(ShopItem shopItem)
        {
            switch (shopItem.Unit)
            {
                case Unit.l:
                    shopItem.Unit = Unit.ml;
                    shopItem.Quantity = shopItem.Quantity * 1000;
                    break;
                case Unit.cup:
                    shopItem.Unit = Unit.ml;
                    shopItem.Quantity = shopItem.Quantity * 236.588;
                    break;
                case Unit.tbsp:
                    shopItem.Unit = Unit.ml;
                    shopItem.Quantity = shopItem.Quantity * 17.7582;
                    break;
                case Unit.tsp:
                    shopItem.Unit = Unit.ml;
                    shopItem.Quantity = shopItem.Quantity * 5.919400003138;
                    break;
                case Unit.pinch:
                    shopItem.Unit = Unit.ml;
                    shopItem.Quantity = shopItem.Quantity * 0.31;
                    break;
                case Unit.kg:
                    shopItem.Unit = Unit.g;
                    shopItem.Quantity = shopItem.Quantity * 1000;
                    break;
            }

            return shopItem;
        }

        public static List<ShopItem> CombineShopItems(List<ShopItem> shopItems)
        {
            // Get a distinct list of different shop items
            var distinctProductsAndUnits = shopItems.Select(si => new { si.ProductId, si.Unit } ).Distinct().ToList();

            // Initialise a new list of shopitems
            var combinedShopItems = new List<ShopItem>();

            foreach (var product in distinctProductsAndUnits)
            {
                // Get all shop items of this product type
                var productShopItems = shopItems.Where(si => si.ProductId == product.ProductId && si.Unit == product.Unit);

                if (productShopItems.Any())
                {
                    // Get meta from first item (unit etc.)
                    var exampleShopItem = productShopItems.FirstOrDefault();
                    var unit = product.Unit;

                    // create a new shop item of this product type to contain all of them
                    var newShopItem = new ShopItem()
                    {
                        Name = exampleShopItem.Name,
                        ProductId = exampleShopItem.ProductId,
                        Product = exampleShopItem.Product,
                        Unit = unit,
                        Quantity = 0
                    };

                    // Add all shop items of this type together in new shop item
                    foreach (var shopItem in productShopItems)
                    {
                        newShopItem.Quantity += ConvertToStandardUnits(shopItem).Quantity;
                    }

                    combinedShopItems.Add(newShopItem);
                }
            }

            return combinedShopItems;
        }
    }

    public enum Unit
    {
        unit, // none
        l, // litres
        g, // grams
        cup, // cups
        tbsp, // 
        tsp,
        pinch,
        ml,
        kg
    }
}
