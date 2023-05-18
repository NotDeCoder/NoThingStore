using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoThingStore.Data;
using NoThingStore.Models;

namespace NoThingStore.Controllers
{
    public class ActivationKeysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivationKeysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ActivationKeys
        public async Task<IActionResult> Index()
        {
            return _context.ActivationKeys != null ?
                          View(await _context.ActivationKeys.Include(ak => ak.ProductImages).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ActivationKeys'  is null.");
        }

        // GET: ActivationKeys/Details/5
        [Route("{controller}/Details/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ActivationKeys == null)
            {
                return NotFound();
            }

            var activationKey = await _context.ActivationKeys
                .Include(ak => ak.ProductImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (activationKey == null)
            {
                return NotFound();
            }

            return View(activationKey);
        }

        // GET: ActivationKeys/slug
        [Route("{controller}/{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            if (slug == null || _context.ActivationKeys == null)
            {
                return NotFound();
            }
            var activationKey = await _context.ActivationKeys
                .Include(ak => ak.ProductImages)
                .FirstOrDefaultAsync(m => m.Slug == slug);
            if (activationKey == null)
            {
                return NotFound();
            }
            return View("Details", activationKey);
        }

        // GET: ActivationKeys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ActivationKeys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Key,TargetProgramName,ProgramVersion,ExpirationDate,HowToActivate,Id,Name,Slug,ShortDescription,LongDescription,Price,IsAvailable")] ActivationKey activationKey)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activationKey);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(activationKey);
        }

        // GET: ActivationKeys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ActivationKeys == null)
            {
                return NotFound();
            }

            var activationKey = await _context.ActivationKeys.FindAsync(id);
            if (activationKey == null)
            {
                return NotFound();
            }
            return View(activationKey);
        }

        // POST: ActivationKeys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Key,TargetProgramName,ProgramVersion,ExpirationDate,HowToActivate,Id,Name,Slug,ShortDescription,LongDescription,Price,IsAvailable")] ActivationKey activationKey)
        {
            if (id != activationKey.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activationKey);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivationKeyExists(activationKey.Id))
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
            return View(activationKey);
        }

        // GET: ActivationKeys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ActivationKeys == null)
            {
                return NotFound();
            }

            var activationKey = await _context.ActivationKeys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activationKey == null)
            {
                return NotFound();
            }

            return View(activationKey);
        }

        // POST: ActivationKeys/Delete/5
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ActivationKeys == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ActivationKeys'  is null.");
            }
            var activationKey = await _context.ActivationKeys.FindAsync(id);
            if (activationKey != null)
            {
                _context.ActivationKeys.Remove(activationKey);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivationKeyExists(int id)
        {
            return (_context.ActivationKeys?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
