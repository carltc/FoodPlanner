using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodPlanner.Data;
using FoodPlanner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FoodPlanner.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ChangeActiveHouseholdModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<ChangeActiveHouseholdModel> _logger;
        private readonly FoodPlannerContext _context_foodplanner;

        public ChangeActiveHouseholdModel(UserManager<AppUser> userManager, ILogger<ChangeActiveHouseholdModel> logger, FoodPlannerContext context_foodplanner)
        {
            _userManager = userManager;
            _logger = logger;
            _context_foodplanner = context_foodplanner;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(int activeHouseholdId, string returnUrl = null)
        {
            var user = await _userManager.GetUserAsync(User);
            user.SetActiveHouseholdId(_context_foodplanner, activeHouseholdId);

            _logger.LogInformation("Active Household changed.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
