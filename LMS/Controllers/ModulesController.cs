using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS.Data;
using LMS.Models;

namespace LMS.Controllers
{
    public class ModulesController : Controller
    {
        private readonly ApplicationDbContext db;

        public ModulesController(ApplicationDbContext context)
        {
            db = context;
        }

        // GET: Module List
        public async Task<IActionResult> GetModulesByCourse(int Id)
        {
            var moduleList = await db.Modules
                .Include(a => a.Activities)
                .Where(c => c.CourseId == Id)
                .OrderBy(m=>m.StartDate)
                //.ThenBy(m=>m.Activities.)
                .ToListAsync();
            var course = await db.Courses.Where(x => x.Id == Id).SingleAsync();
            var model = new Models.ViewModels.ModuleListViewModel
            {
                Course = course,
                ModuleList = moduleList
            };
            return PartialView("ModuleListPartial", model);
        }


        // GET: Modules
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = db.Modules.Include(x => x.Course);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Modules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await db.Modules
                .Include(x => x.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }


        // GET: Modules/PartialDetails/5
        public async Task<IActionResult> PartialDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var module = await db.Modules
                .FirstOrDefaultAsync(a => a.Id == id);
            if (module == null)
            {
                return NotFound();
            }

            return PartialView("ModulePartialDetails", module);
        }

        // GET: Modules/Create
        public IActionResult Create(int? id)
        {
            //ViewData["CourseId"] = new SelectList(db.Courses, "Id", "Name");
            var module = new Module();
            module.CourseId = (int)id;
            module.StartDate = DateTime.Now;
            module.EndDate = DateTime.Now;
            //ViewData["CourseId"] = id;
            return View(module);
        }

        // POST: Modules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,StartDate,EndDate,CourseId")] Module @module)
        {
            if (ModelState.IsValid)
            {
                db.Add(@module);
                await db.SaveChangesAsync();
                return RedirectToAction("OverView", "Courses");
            }
            ViewData["CourseId"] = new SelectList(db.Courses, "Id", "Name", @module.CourseId);
            return View(@module);
        }

        // GET: Modules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await db.Modules.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(db.Courses, "Id", "Name", @module.CourseId);
            return View(@module);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartDate,EndDate,CourseId")] Module @module)
        {
            if (id != @module.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(@module);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(@module.Id))
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
            ViewData["CourseId"] = new SelectList(db.Courses, "Id", "Id", @module.CourseId);
            return View(@module);
        }

        // GET: Modules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await db.Modules
                .Include(x => x.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @module = await db.Modules.FindAsync(id);
            db.Modules.Remove(@module);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuleExists(int id)
        {
            return db.Modules.Any(e => e.Id == id);
        }
    }
}
