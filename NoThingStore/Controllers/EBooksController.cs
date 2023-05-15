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
                        View(await _context.EBooks.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.EBooks' is null.");
        }

        // GET: EBooks/Details/5
        public async Task<IActionResult> Details(string? id)
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

        // GET: EBooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MegabyteSize,Authorship,Format,CountOfPages,Id,Name,Slug,Description,Price,IsAvailable,DownloadUrl")] EBook eBook)
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
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.EBooks == null)
            {
                return NotFound();
            }

            var eBook = await _context.EBooks.FindAsync(id);
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
        public async Task<IActionResult> Edit(string id, [Bind("MegabyteSize,Authorship,Format,CountOfPages,Id,Name,Slug,Description,Price,IsAvailable,DownloadUrl")] EBook eBook)
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
        public async Task<IActionResult> Delete(string? id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
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

        private bool EBookExists(string id)
        {
            return (_context.EBooks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
