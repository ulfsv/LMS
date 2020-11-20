using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LMS.Data;
using LMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMS.Services
{
    public class NextActivityService : INextActivityService
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        public NextActivityService(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            this.db = applicationDbContext;
            this.userManager = userManager;
        }

        public Aktivitet GetActivity(ClaimsPrincipal user)
        {
            var userId = userManager.GetUserId(user);
            var userModules = db.Users.Where(u => u.Id == userId).Include(u => u.Course)
                .ThenInclude(c => c.Modules).ThenInclude(m => m.Activities)
                .SelectMany(u => u.Course.Modules);

            var activities = new List<Aktivitet>();
            foreach (var userModule in userModules)
            {
                activities.AddRange(userModule.Activities);
            }
            var sortedActivities = activities.OrderBy(a => a.StartTime);

            var nextActivity = sortedActivities.FirstOrDefault(a => a.StartTime > DateTime.Now);
            return nextActivity;
        }
    }
}
