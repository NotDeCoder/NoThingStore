using Microsoft.AspNetCore.Mvc;
using NoThingStore.Models;

namespace NoThingStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ShoppingCart _shoppingCart;

        public CartController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _shoppingCart = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<ShoppingCart>("ShoppingCart") ?? new ShoppingCart();
        }

        public IActionResult Index()
        {
            return View(_shoppingCart);
        }

        public IActionResult AddToCart(int productId, string name, decimal price, int quantity = 1)
        {
            var cartItem = new CartItem { ProductId = productId, Name = name, Price = price, Quantity = quantity };
            _shoppingCart.AddItem(cartItem);
            _httpContextAccessor.HttpContext.Session.SetObjectAsJson("ShoppingCart", _shoppingCart);
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
