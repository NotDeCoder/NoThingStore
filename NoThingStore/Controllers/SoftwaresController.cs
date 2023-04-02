using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoThingStore.Models;
using NoThingStore.Data;

namespace NoThingStore.Controllers
{
    public class SoftwaresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SoftwaresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Softwares
        public async Task<IActionResult> Index()
        {
            return _context.Softwares != null ?
                        View(await _context.Softwares.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Softwares'  is null.");
        }

        // GET: Softwares/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Softwares == null)
            {
                return NotFound();
            }

            var software = await _context.Softwares
                .FirstOrDefaultAsync(m => m.Id == id);
            if (software == null)
            {
                return NotFound();
            }

            return View(software);
        }

        // GET: Softwares/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Softwares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MegabyteSize,HowToInstall,SystemRequirement,Authorship,Id,Name,Slug,Description,Price,IsAvailable,DownloadUrl")] Software software)
        {
            if (ModelState.IsValid)
            {
                _context.Add(software);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(software);
        }

        // GET: Softwares/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Softwares == null)
            {
                return NotFound();
            }

            var software = await _context.Softwares.FindAsync(id);
            if (software == null)
            {
                return NotFound();
            }
            return View(software);
        }

        // POST: Softwares/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MegabyteSize,HowToInstall,SystemRequirement,Authorship,Id,Name,Slug,Description,Price,IsAvailable,DownloadUrl")] Software software)
        {
            if (id != software.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(software);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoftwareExists(software.Id))
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
            return View(software);
        }

        // GET: Softwares/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Softwares == null)
            {
                return NotFound();
            }

            var software = await _context.Softwares
                .FirstOrDefaultAsync(m => m.Id == id);
            if (software == null)
            {
                return NotFound();
            }

            return View(software);
        }

        // POST: Softwares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Softwares == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Softwares'  is null.");
            }
            var software = await _context.Softwares.FindAsync(id);
            if (software != null)
            {
                _context.Softwares.Remove(software);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoftwareExists(int id)
        {
            return (_context.Softwares?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
