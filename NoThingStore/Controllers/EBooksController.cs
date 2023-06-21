using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoThingStore.Data;
using NoThingStore.Models;

namespace NoThingStore.Controllers
{
    public class EBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EBooks
        public async Task<IActionResult> Index()
        {
            return _context.EBooks != null ?
                        View(await _context.EBooks.Include(eb => eb.ProductImages).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.EBooks'  is null.");
        }

        // GET: EBooks/Details/5
        [Route("{controller}/Details/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EBooks == null)
            {
                return NotFound();
            }

            var eBook = await _context.EBooks
                .Include(eb => eb.ProductImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eBook == null)
            {
                return NotFound();
            }

            return View(eBook);
        }

        // GET: EBooks/slug
        [Route("{controller}/{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            if (slug == null || _context.EBooks == null)
            {
                return NotFound();
            }
            var eBook = await _context.EBooks
                .Include(eb => eb.ProductImages)
                .FirstOrDefaultAsync(m => m.Slug == slug);
            if (eBook == null)
            {
                return NotFound();
            }
            return View("Details", eBook);
        }

        // GET: EBooks/Create
        [HttpGet]
        [Route("EBooks/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: EBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("EBooks/Create")]
        public async Task<IActionResult> Create(EBook eBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eBook);
        }

        // GET: EBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EBooks == null)
            {
                return NotFound();
            }

            var eBook = await _context.EBooks
                .Include(eb => eb.ProductImages)
                .FirstOrDefaultAsync(eb => eb.Id == id);

            if (eBook == null)
            {
                return NotFound();
            }

            return View(eBook);
        }

        // POST: EBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MegabyteSize,Authorship,Format,CountOfPages,DownloadUrl,Id,Name,Slug,ShortDescription,LongDescription,Price,IsAvailable")] EBook eBook)
        {
            if (id != eBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EBookExists(eBook.Id))
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
            return View(eBook);
        }

        // GET: EBooks/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EBooks == null)
            {
                return NotFound();
            }

            var eBook = await _context.EBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eBook == null)
            {
                return NotFound();
            }

            return View(eBook);
        }

        // POST: EBooks/Delete/5
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.EBooks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EBooks'  is null.");
            }
            var eBook = await _context.EBooks.FindAsync(id);
            if (eBook != null)
            {
                _context.EBooks.Remove(eBook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EBookExists(int id)
        {
            return (_context.EBooks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
