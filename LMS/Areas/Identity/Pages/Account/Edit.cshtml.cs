using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LMS.Data;
using LMS.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LMS.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Teacher")]
    public class EditModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<EditModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext db;
        public EditModel(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<EditModel> logger,
            IEmailSender emailSender, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            db = context;
        }

        [BindProperty]
        public ApplicationUser AppUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            AppUser = await db.ApplicationUsers.FindAsync(id);       
            if (AppUser == null)
            {
                return RedirectToPage("Index");
            }
            ViewData["CourseId"] = new SelectList(db.Courses, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var changingUser = await db.ApplicationUsers.FindAsync(id);
            changingUser.FirstName = AppUser.FirstName;
            changingUser.LastName = AppUser.LastName;
            changingUser.UserName = AppUser.Email;
            changingUser.Email = AppUser.Email;
            changingUser.CourseId = AppUser.CourseId;
            await _userManager.UpdateAsync(changingUser);

            return LocalRedirect("/Courses/Overview");
        }
    }
}

