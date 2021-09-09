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
    public partial class Create_HouseholdsModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly FoodPlannerContext _context_foodplanner;

        public Create_HouseholdsModel(UserManager<AppUser> userManager, FoodPlannerContext context_foodplanner)
        {
            _userManager = userManager;
            _context_foodplanner = context_foodplanner;
        }

        [BindProperty]
        public string HouseholdName { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (HouseholdName == null)
            {
                StatusMessage = $"No household name supplied.";
                return RedirectToPage();
            }

            // Check if household with this name exists
            if (!_context_foodplanner.Households.Where(hh => hh.Name == HouseholdName).Any())
            {
                // If it does NOT, then create the household and add user to this household
                var household = new Household()
                {
                    Name = HouseholdName
                };
                household.HouseholdUsers = new List<HouseholdUser>();
                household.HouseholdUsers.Add(
                    new HouseholdUser()
                    {
                        AppUserId = user.Id,
                        HouseholdId = household.Id
                    }
                );

                _context_foodplanner.Households.Add(household);
                await _context_foodplanner.SaveChangesAsync();
                return RedirectToPage("./Households");
            }

            StatusMessage = $"Household with this name '{HouseholdName}' already exists.";
            return RedirectToPage();
        }
    }
}
