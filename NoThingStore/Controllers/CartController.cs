using Microsoft.AspNetCore.Mvc;
using NoThingStore.Models;
using NoThingStore.Services.Interfaces;

namespace NoThingStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductService _productService;
        private readonly ShoppingCart _shoppingCart;

        public CartController(IProductService productService, IHttpContextAccessor httpContextAccessor)
        {
            _productService = productService;
            _httpContextAccessor = httpContextAccessor;
            _shoppingCart = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<ShoppingCart>("ShoppingCart") ?? new ShoppingCart();
        }

        public IActionResult Index()
        {
            return View(_shoppingCart);
        }

        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            var product = _productService.GetProductByIdAsync(productId).Result;
            if (product != null)
            {
                _shoppingCart.AddItem(product, quantity);
                _httpContextAccessor.HttpContext.Session.SetObjectAsJson("ShoppingCart", _shoppingCart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            _shoppingCart.RemoveItem(productId);
            _httpContextAccessor.HttpContext.Session.SetObjectAsJson("ShoppingCart", _shoppingCart);
            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            _shoppingCart.Clear();
            _httpContextAccessor.HttpContext.Session.SetObjectAsJson("ShoppingCart", _shoppingCart);
            return RedirectToAction("Index");
        }
    }
}
