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
    public class ActivitiesController : Controller
    {
        private readonly ApplicationDbContext db;

        public ActivitiesController(ApplicationDbContext context)
        {
            db = context;
        }

        // GET: Aktivitets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = db.Activities.Include(a => a.ActivityType).Include(a => a.Module);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Aktivitets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktivitet = await db.Activities
                .Include(a => a.ActivityType)
                .Include(a => a.Module)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aktivitet == null)
            {
                return NotFound();
            }

            return View(aktivitet);
        }

        // GET: Activities/PartialDetails/5
        public async Task<IActionResult> PartialDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await db.Activities
                .Include(a=>a.ActivityType)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (activity == null)
            {
                return NotFound();
            }

            return PartialView("ActivityPartialDetails", activity);
        }

        // GET: Aktivitets/Create
        public IActionResult Create(int? id)
        {
            var activity = new Aktivitet();
            activity.ModuleId = (int)id;
            activity.StartTime = DateTime.Now;
            activity.EndTime = DateTime.Now;
            ViewData["ActivityTypeId"] = new SelectList(db.ActivityTypes, "Id", "TypeName");
            return View(activity);
        }

        // POST: Aktivitets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,StartTime,EndTime,ModuleId,ActivityTypeId")] Aktivitet aktivitet)
        {
            if (ModelState.IsValid)
            {
                db.Add(aktivitet);
                await db.SaveChangesAsync();
                return RedirectToAction("Overview", "Courses");
            }
            ViewData["ActivityTypeId"] = new SelectList(db.ActivityTypes, "Id", "TypeName", aktivitet.ActivityTypeId);
            //ViewData["ModuleId"] = new SelectList(db.Modules, "Id", "Id", aktivitet.ModuleId);
            return View(aktivitet);
        }

        // GET: Aktivitets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktivitet = await db.Activities.FindAsync(id);
            if (aktivitet == null)
            {
                return NotFound();
            }
            ViewData["ActivityTypeId"] = new SelectList(db.ActivityTypes, "Id", "TypeName", aktivitet.ActivityTypeId);
            ViewData["ModuleId"] = new SelectList(db.Modules, "Id", "Name", aktivitet.ModuleId);
            return View(aktivitet);
        }

        // POST: Aktivitets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartTime,EndTime,ModuleId,ActivityTypeId")] Aktivitet aktivitet)
        {
            if (id != aktivitet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(aktivitet);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AktivitetExists(aktivitet.Id))
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
            ViewData["ActivityTypeId"] = new SelectList(db.ActivityTypes, "Id", "Id", aktivitet.ActivityTypeId);
            ViewData["ModuleId"] = new SelectList(db.Modules, "Id", "Id", aktivitet.ModuleId);
            return View(aktivitet);
        }

        // GET: Aktivitets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktivitet = await db.Activities
                .Include(a => a.ActivityType)
                .Include(a => a.Module)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aktivitet == null)
            {
                return NotFound();
            }

            return View(aktivitet);
        }

        // POST: Aktivitets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aktivitet = await db.Activities.FindAsync(id);
            db.Activities.Remove(aktivitet);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AktivitetExists(int id)
        {
            return db.Activities.Any(e => e.Id == id);
        }
    }
}
