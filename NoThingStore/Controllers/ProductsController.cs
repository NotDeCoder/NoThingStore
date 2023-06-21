using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using NoThingStore.Data;

namespace NoThingStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task RemoveProductImage(int? id)
        {
            if (id == null || _context.ProductImages == null)
            {
                NotFound();
            }

            var productImage = await _context.ProductImages
                .FirstOrDefaultAsync(m => m.Id == id);

            if (productImage == null)
            {
                NotFound();
            }

            _context.ProductImages.Remove(productImage);

            await _context.SaveChangesAsync();

            Response.Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
