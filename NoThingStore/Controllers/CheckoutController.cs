using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoThingStore.Models;
using NoThingStore.Services.Interfaces;
using System.Security.Claims;

namespace NoThingStore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ShoppingCart _shoppingCart;

        public CheckoutController(IOrderService orderService, ShoppingCart shoppingCart)
        {
            _orderService = orderService;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Order order)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (userId == null || userEmail == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (_shoppingCart.Items.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty.");
            }

            if (ModelState.IsValid)
            {
                order.UserId = userId;
                order.Email = userEmail;
                order.OrderDate = DateTime.Now;
                order.TotalPrice = _shoppingCart.GetTotalPrice();

                var createdOrder = await _orderService.CreateOrderAsync(userId, userEmail, _shoppingCart);
                await _orderService.SendOrderConfirmationEmail(createdOrder);

                _shoppingCart.Clear();
                return RedirectToAction("Completed", "Checkout");
            }

            return View(order);
        }

        public IActionResult Completed()
        {
            return View();
        }
    }
}
