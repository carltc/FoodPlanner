using FoodPlanner.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Models
{
    public class Household
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<HouseholdUser> HouseholdUsers { get; set; }

        //public Household AuthenticateHouseholdUser(string UserEmail, FoodPlannerContext context)
        //{
        //    if (context.Household.Count() > 0 && context.HouseholdUser.Where(hu => hu.UserEmail == UserEmail).Count() > 0)
        //    {
        //        var householdId = context.HouseholdUser.Where(hu => hu.UserEmail == UserEmail).FirstOrDefault().HouseholdId;
        //        var household = context.Household.Where(hh => hh.Id == householdId).FirstOrDefault();

        //        if (household != null)
        //        {
        //            return household;
        //        }
        //    }

        //    return new Household()
        //    {
        //        Name = "None"
        //    };
        //}
    }
}
