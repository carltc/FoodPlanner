using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public static class ShoppingLists
    {
        public static Dictionary<int, ShoppingList> Household_ShoppingLists = new Dictionary<int, ShoppingList>();

        public static bool HasHouseholdList(int householdId)
        {
            return Household_ShoppingLists.Keys.Contains(householdId);
        }
    }

    public enum ShoppingListSortType
    {
        Category,
        FoodPlan
    }
}
