using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FoodPlanner.Data;
using FoodPlanner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FoodPlanner.Areas.Identity.Pages.Account.Manage
{
    public partial class HouseholdsModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly FoodPlannerContext _context_foodplanner;

        public HouseholdsModel(UserManager<AppUser> userManager, FoodPlannerContext context_foodplanner)
        {
            _userManager = userManager;
            _context_foodplanner = context_foodplanner;
        }

        public List<Household> Households { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public int LeaveHouseholdId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Get all households for this user
            var user_with_content = _context_foodplanner.Users.Where(u => u.Id == user.Id).Include(u => u.HouseholdUsers).ThenInclude(hu => hu.Household).FirstOrDefault();
            var households = user_with_content.HouseholdUsers.Select(hu => hu.Household);
            Households = households.ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Check if household-to-leave id has been sent
            if (LeaveHouseholdId != 0)
            {
                var householduser = _context_foodplanner.HouseholdUsers.Where(hu => hu.AppUserId == user.Id && hu.HouseholdId == LeaveHouseholdId).Include(hu => hu.Household).First();

                if (householduser != null)
                {
                    string householdname = householduser.Household.Name;
                    _context_foodplanner.HouseholdUsers.Remove(householduser);
                    await _context_foodplanner.SaveChangesAsync();

                    StatusMessage = "You have left household '" + householdname + "'.";
                }
            }

            return RedirectToPage();
        }
    }
}
