using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS.Data;
using LMS.Models;
using LMS.Models.ViewModels;

namespace LMS.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string noDocumentsMessage = "No documents";

        public DocumentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Documents
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Documents.Include(d => d.Activity).Include(d => d.ApplicationUser).Include(d => d.Course).Include(d => d.Module);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Documents for course
        public async Task<IActionResult> GetForCourse(int id)
        {
            var applicationDbContext = _context.Documents
                                       .Where(d => d.CourseId == id);
            var model = new DocumentListViewModel();
            model.TypeHeader = "Course Documents";
            model.Documents = await applicationDbContext.ToListAsync();
            model.EmptyMessage = (model.Documents.Count == 0) ? noDocumentsMessage : "";

            return PartialView("DocumentPartialList", model);
        }

        // GET: Documents for module

        public async Task<IActionResult> GetForModule(int id)
        {
            var applicationDbContext = _context.Documents
                                       .Where(d => d.ModuleId == id);
            var model = new DocumentListViewModel();
            model.TypeHeader = "Module Documents";
            model.Documents = await applicationDbContext.ToListAsync();
            model.EmptyMessage = (model.Documents.Count == 0) ? noDocumentsMessage : "";

            return PartialView("DocumentPartialList", model);
        }

        // GET: Documents for activity
        public async Task<IActionResult> GetForActivity(int id)
        {
            var applicationDbContext = _context.Documents
                                       .Where(d => d.ActivityId == id);
            var model = new DocumentListViewModel();
            model.TypeHeader = "Activity Documents";
            model.Documents = await applicationDbContext.ToListAsync();
            model.EmptyMessage = (model.Documents.Count == 0) ? noDocumentsMessage : "";

            return PartialView("DocumentPartialList", model);
        }

        // GET: Documents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents
                .Include(d => d.Activity)
                .Include(d => d.ApplicationUser)
                .Include(d => d.Course)
                .Include(d => d.Module)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // GET: Documents/Create
        public IActionResult Create()
        {
            ViewData["ActivityId"] = new SelectList(_context.Activities, "Id", "Name");
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "UserName");
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Name");
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,UploadTimeStamp,Storage,CourseId,ModuleId,ActivityId,ApplicationUserId")] Document document)
        {
            if (ModelState.IsValid)
            {
                _context.Add(document);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityId"] = new SelectList(_context.Activities, "Id", "Name", document.ActivityId);
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "UserName", document.ApplicationUserId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", document.CourseId);
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Name", document.ModuleId);
            return View(document);
        }

        // GET: Documents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            ViewData["ActivityId"] = new SelectList(_context.Activities, "Id", "Name", document.ActivityId);
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "UserName", document.ApplicationUserId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", document.CourseId);
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Name", document.ModuleId);
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,UploadTimeStamp,Storage,CourseId,ModuleId,ActivityId,ApplicationUserId")] Document document)
        {
            if (id != document.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(document);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentExists(document.Id))
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
            ViewData["ActivityId"] = new SelectList(_context.Activities, "Id", "Name", document.ActivityId);
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "UserName", document.ApplicationUserId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", document.CourseId);
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Name", document.ModuleId);
            return View(document);
        }

        // GET: Documents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents
                .Include(d => d.Activity)
                .Include(d => d.ApplicationUser)
                .Include(d => d.Course)
                .Include(d => d.Module)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentExists(int id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }
    }
}
