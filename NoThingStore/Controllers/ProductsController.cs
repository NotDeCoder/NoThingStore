using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using NoThingStore.Data;
using NoThingStore.Models;

namespace NoThingStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task RemoveImageFromProduct(int? id)
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

        [HttpPost]
        public async Task AddImageToProduct(string url, int productId)
        {
            if (string.IsNullOrEmpty(url) || productId == 0)
            {
                NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == productId);

            if (product == null)
            {
                NotFound();
            }

            var productImage = new ProductImage
            {
                ProductId = productId,
                ImageUrl = url
            };

            _context.ProductImages.Add(productImage);

            await _context.SaveChangesAsync();

            Response.Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
