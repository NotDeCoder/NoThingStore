using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoThingStore.Data;
using NoThingStore.Models;

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
                        View(await _context.Softwares.Include(sw => sw.ProductImages).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Softwares'  is null.");
        }

        // GET: Softwares/Details/5
        [Route("{controller}/Details/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Softwares == null)
            {
                return NotFound();
            }

            var software = await _context.Softwares
                .Include(sw => sw.ProductImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (software == null)
            {
                return NotFound();
            }

            return View(software);
        }

        // GET: Softwares/slug
        [Route("{controller}/{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            if (slug == null || _context.Softwares == null)
            {
                return NotFound();
            }
            var software = await _context.Softwares
                .Include(sw => sw.ProductImages)
                .FirstOrDefaultAsync(m => m.Slug == slug);
            if (software == null)
            {
                return NotFound();
            }
            return View(software);
        }

        // GET: Softwares/Create
        [HttpGet]
        [Route("Softwares/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Softwares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Softwares/Create")]
        public async Task<IActionResult> Create(Software software)
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

            var software = await _context.Softwares
                .Include(sw => sw.ProductImages)
                .FirstOrDefaultAsync(sw => sw.Id == id);

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
        public async Task<IActionResult> Edit(int id, [Bind("MegabyteSize,HowToInstall,SystemRequirement,Authorship,DownloadUrl,Id,Name,Slug,ShortDescription,LongDescription,Price,IsAvailable")] Software software)
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
        [HttpGet]
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
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
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
