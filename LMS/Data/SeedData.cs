using Bogus;
using LMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Data
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services, string adminPW)
        {
            var rnd = new Random();

            using (var context = new ApplicationDbContext(services.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Courses.Any()) return;

                var fake = new Faker();

                var courses = new List<Course>();

                for (int i = 1; i < 21; i++)
                {
                    var course = new Course
                    {
                        Name = fake.Company.CatchPhrase(),
                        Description = fake.Hacker.Verb(),
                        //Duration = new TimeSpan(0, 55, 0),
                        StartDate = DateTime.Now.AddDays(fake.Random.Int(-2, 2))
                    };
                    courses.Add(course);
                }
                await context.AddRangeAsync(courses);
                await context.SaveChangesAsync();

                var userManger = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManger = services.GetRequiredService<RoleManager<IdentityRole>>();

                var roleNames = new[] { "Teacher", "Student" };

                foreach (var roleName in roleNames)
                {
                    if (await roleManger.RoleExistsAsync(roleName)) continue;

                    var role = new IdentityRole { Name = roleName };
                    var result = await roleManger.CreateAsync(role);

                    if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
                }

                for (int i = 0; i < 5; i++)
                {
                    var fName = fake.Name.FirstName();
                    var lName = fake.Name.LastName();
                    var teacher = new ApplicationUser
                    {
                        FirstName = fName,
                        LastName = lName,
                        Email = fake.Internet.Email($"{fName} {lName}"),
                        UserName = fake.Internet.Email($"{fName} {lName}"),
                        CourseId = courses[rnd.Next(courses.Count)].Id
                    };
                    var results = await userManger.CreateAsync(teacher, adminPW);
                    var results2 = await userManger.AddToRoleAsync(teacher, "Teacher");
                }

                for (int i = 0; i < 25; i++)
                {
                    var fName = fake.Name.FirstName();
                    var lName = fake.Name.LastName();
                    var student = new ApplicationUser
                    {
                        FirstName = fName,
                        LastName = lName,
                        Email = fake.Internet.Email($"{fName} {lName}"),
                        UserName = fake.Internet.Email($"{fName} {lName}"),
                        CourseId = courses[rnd.Next(courses.Count)].Id
                    };
                    await userManger.CreateAsync(student, adminPW);
                    await userManger.AddToRoleAsync(student, "Student");
                }

                await context.SaveChangesAsync();
                // seed Modules
                var modules = new List<Module>();
                foreach (var course in courses)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        var module = new Module
                        {
                            Name = fake.Company.CatchPhrase(),
                            Description = fake.Hacker.Verb(),
                            //Duration = new TimeSpan(0, 55, 0),
                            StartDate = course.StartDate.AddDays(i * 28),
                            EndDate = course.StartDate.AddDays(i * 28 + 27),
                            CourseId = course.Id
                        };
                        modules.Add(module);
                    }
                }

                await context.AddRangeAsync(modules);
                await context.SaveChangesAsync();
                // seed ActivityTypes 
                var types = new[] { "Lecture", "Exercise", "E-learning", "Assignment" };
                var activityTypes = new List<ActivityType>();
                foreach (var type in types)
                {
                    var activityType = new ActivityType
                    {
                        TypeName = type
                    };
                    activityTypes.Add(activityType);
                }
                await context.AddRangeAsync(activityTypes);
                await context.SaveChangesAsync();

                // seed Activity
                var activities = new List<Aktivitet>();

                foreach (var module in modules)

                {
                    for (int i = 0; i < 7; i++)
                    {
                        var activity = new Aktivitet
                        {
                            Name = fake.Company.CatchPhrase(),
                            Description = fake.Hacker.Verb(),
                            ModuleId = module.Id,
                            StartTime = module.StartDate.AddDays(i * 4),
                            EndTime = module.StartDate.AddDays(i * 4 + 3),
                            ActivityTypeId = activityTypes[rnd.Next(activityTypes.Count)].Id                           
                        };
                        activities.Add(activity);
                    }
                }

                await context.AddRangeAsync(activities);
                await context.SaveChangesAsync();
                
            }
        }
    }
}