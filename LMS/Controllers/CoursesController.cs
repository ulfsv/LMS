﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS.Data;
using LMS.Models;
using LMS.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using IdentityModel;

namespace LMS.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public CoursesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            db = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        // Teacher OverView
        //[Authorize(Roles = "Teacher")]
        public async Task<IActionResult> TeacherOverView()
        {
            var model = new TeacherOverViewModel();

            var userId = userManager.GetUserId(User);
            var courseId = await db.ApplicationUsers.Where(u=>u.Id==userId)
                .Select(u=>u.CourseId).SingleAsync();
            var attendingCourse = await db.Courses.Where(c=> c.Id == courseId).ToListAsync();

            //if (!model.showallcourses)
            //    model.courses = attendingcourse;
            //if (model.showallcourses)
                model.Courses = await db.Courses.ToListAsync();

            return View(model);
        }
        // END Teacher OverView

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            return View(await db.Courses.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await db.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/PartialDetails/5
        public async Task<IActionResult> PartialDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await db.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return PartialView("CoursePartialDetails", course);
        }

        //Student List
        // GET: Student/List/5
        public async Task<IActionResult> GetStudentsList(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await userManager.GetUsersInRoleAsync("Student");
            var participatingStudents = students.Where(s => s.CourseId == id).ToList();

            return PartialView("StudentsPartialList", participatingStudents);
        }

        // GET: Student/PartialDetails/5
        
        // END Student List

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,StartDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Add(course);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartDate")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(course);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await db.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await db.Courses.FindAsync(id);
            db.Courses.Remove(course);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return db.Courses.Any(e => e.Id == id);
        }
    }
}
