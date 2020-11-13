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
                //if (context.Courses.Any()) return;
                var fake = new Faker();

                var courses = new List<Course>();

                for (int i = 0; i < 20; i++)
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
                        Course = courses[rnd.Next(courses.Count)]
                    };
                    var results = await userManger.CreateAsync(teacher);
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
                        Course = courses[rnd.Next(courses.Count)]
                    };
                    await userManger.CreateAsync(student);
                    await userManger.AddToRoleAsync(student, "Student");
                }

                await context.SaveChangesAsync();
            }
        }
    }
}