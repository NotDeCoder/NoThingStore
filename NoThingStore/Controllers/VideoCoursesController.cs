using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NoThingStore.Data;
using NoThingStore.Models;

namespace NoThingStore.Controllers
{
    public class VideoCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VideoCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VideoCourses
        public async Task<IActionResult> Index()
        {
              return _context.VideoCourses != null ? 
                          View(await _context.VideoCourses.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.VideoCourses'  is null.");
        }

        // GET: VideoCourses/Details/5
        [Route("{controller}/Details/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VideoCourses == null)
            {
                return NotFound();
            }

            var videoCourse = await _context.VideoCourses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoCourse == null)
            {
                return NotFound();
            }

            return View(videoCourse);
        }

        // GET: VideoCourses/slug
        [Route("{controller}/{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            if (slug == null || _context.VideoCourses == null)
            {
                return NotFound();
            }
            var videoCourse = await _context.VideoCourses
                .FirstOrDefaultAsync(m => m.Slug == slug);
            if (videoCourse == null)
            {
                return NotFound();
            }
            return View(videoCourse);
        }

        // GET: VideoCourses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VideoCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountOfVideos,Format,Duration,Language,DownloadUrl,Id,Name,Slug,ShortDescription,LongDescription,Price,IsAvailable")] VideoCourse videoCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(videoCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(videoCourse);
        }

        // GET: VideoCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VideoCourses == null)
            {
                return NotFound();
            }

            var videoCourse = await _context.VideoCourses.FindAsync(id);
            if (videoCourse == null)
            {
                return NotFound();
            }
            return View(videoCourse);
        }

        // POST: VideoCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CountOfVideos,Format,Duration,Language,DownloadUrl,Id,Name,Slug,ShortDescription,LongDescription,Price,IsAvailable")] VideoCourse videoCourse)
        {
            if (id != videoCourse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(videoCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoCourseExists(videoCourse.Id))
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
            return View(videoCourse);
        }

        // GET: VideoCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VideoCourses == null)
            {
                return NotFound();
            }

            var videoCourse = await _context.VideoCourses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoCourse == null)
            {
                return NotFound();
            }

            return View(videoCourse);
        }

        // POST: VideoCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VideoCourses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.VideoCourses'  is null.");
            }
            var videoCourse = await _context.VideoCourses.FindAsync(id);
            if (videoCourse != null)
            {
                _context.VideoCourses.Remove(videoCourse);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoCourseExists(int id)
        {
            return (_context.VideoCourses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
