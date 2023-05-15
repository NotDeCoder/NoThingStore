using Microsoft.AspNetCore.Mvc;
using NoThingStore.Models;
using NoThingStore.Services.Interfaces;
using System.Security.Claims;

public class CheckoutController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IOrderService _orderService;
    private readonly ShoppingCart _shoppingCart;

    public CheckoutController(IOrderService orderService, IHttpContextAccessor httpContextAccessor)
    {
        _orderService = orderService;
        _httpContextAccessor = httpContextAccessor;
        _shoppingCart = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<ShoppingCart>("ShoppingCart") ?? new ShoppingCart();
    }

    public async Task<IActionResult> Index()
    {
        if (_shoppingCart.Items.Count == 0)
        {
            ModelState.AddModelError("", "Your cart is empty.");
            return RedirectToAction("Index", "Home");
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userEmail = User.FindFirstValue(ClaimTypes.Email);

        var order = CreateOrder(userId, userEmail);

        var productIds = _shoppingCart.Items.Select(i => i.ProductId);

        foreach (var item in _shoppingCart.Items)
        {
            var product = await _orderService.GetProductByIdAsync(item.ProductId);
            if (product != null)
            {
                OrderItem orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Product = product,
                    Order = order
                };
                order.OrderItems.Add(orderItem);
            }
        }

        if (order.OrderItems.Count != productIds.Count())
        {
            ModelState.AddModelError("", "Some products from your cart are no longer available.");
            return RedirectToAction("Index", "Home");
        }

        if (!ModelState.IsValid)
        {
            return View(order);
        }

        await _orderService.CreateOrderAsync(order);

        _shoppingCart.Clear();

        return View("Index");
    }

    private Order CreateOrder(string userId, string userEmail)
    {
        return new Order
        {
            UserId = userId,
            Email = userEmail,
            OrderDate = DateTime.Now,
            Total = _shoppingCart.GetTotalPrice(),
            OrderItems = new List<OrderItem>()
        };
    }
}
