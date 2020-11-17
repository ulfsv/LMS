using LMS.Data;
using LMS.Models;
using LMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Controllers
{
    public class ActivityTypesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ActivityTypesController(ApplicationDbContext context)
        {
            _db = context;
        }

        // GET: ActivityTypes
        public async Task<IActionResult> Index()
        {
            return View(await _db.ActivityTypes.ToListAsync());
        }

        // GET: ActivityTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityType = await _db.ActivityTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activityType == null)
            {
                return NotFound();
            }

            return View(activityType);
        }

        // GET: ActivityTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ActivityTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeName")] ActivityType activityType)
        {
            if (ModelState.IsValid)
            {
                _db.Add(activityType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(activityType);
        }

        // GET: ActivityTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityType = await _db.ActivityTypes.FindAsync(id);
            if (activityType == null)
            {
                return NotFound();
            }
            return View(activityType);
        }

        // POST: ActivityTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeName")] ActivityType activityType)
        {
            if (id != activityType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(activityType);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityTypeExists(activityType.Id))
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
            return View(activityType);
        }

        // GET: ActivityTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityType = await _db.ActivityTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activityType == null)
            {
                return NotFound();
            }

            return View(activityType);
        }

        // POST: ActivityTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activityType = await _db.ActivityTypes.FindAsync(id);
            _db.ActivityTypes.Remove(activityType);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityTypeExists(int id)
        {
            return _db.ActivityTypes.Any(e => e.Id == id);
        }

        //Stefan Add Custom Activity Type

        public IActionResult AddNewActivity()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewActivity(AddNewActivityViewModel customActivityModel)
        {
            if (ModelState.IsValid)
            {
                var customActivityType = new ActivityType
                {
                    TypeName = customActivityModel.CustomActivityType
                };
                if (!CustomActivityExist(customActivityModel.CustomActivityType))
                {
                    _db.Add(customActivityType);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    return Json($"{customActivityModel.CustomActivityType} is already in use, please scroll dropdown list!");
                }
            }
            return View(customActivityModel);
        }

        private bool CustomActivityExist(string customActivityType)
        {
            return _db.ActivityTypes.Any(e => e.TypeName == customActivityType);
        }
    }
}
