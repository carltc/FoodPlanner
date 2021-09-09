using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FoodPlanner.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoodPlanner.Models
{
    public class AppUser : IdentityUser
    {
        public List<HouseholdUser> HouseholdUsers { get; set; }

        [NotMapped]
        public List<Household> Households { get; set; }

        public int ActiveHouseholdId { get; set; }

        public bool SetActiveHouseholdId(FoodPlannerContext context, int activeHouseholdId = 0)
        {
            try
            {
                var user = context.Users.Where(u => u.Id == Id).Include(u => u.HouseholdUsers).ThenInclude(hu => hu.Household).First();

                if (activeHouseholdId == 0)
                {
                    // Therefore find all households and set Id to 1st one
                    var households = user.HouseholdUsers.Select(hu => hu.Household).ToList();

                    // Set the active household id of the user
                    user.ActiveHouseholdId = households.First().Id;

                }
                else
                {
                    // Set ActiveHouseholdId to activeHouseholdId
                    user.ActiveHouseholdId = activeHouseholdId;
                }

                // Then save the changes
                context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool LoadHouseholds(FoodPlannerContext context)
        {
            try
            {
                // Get user with content
                var user = context.Users.Where(u => u.Id == Id).Include(u => u.HouseholdUsers).ThenInclude(hu => hu.Household).First();

                // Therefore find all households and set Id to 1st one
                Households = user.HouseholdUsers.Select(hu => hu.Household).ToList();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
