using Microsoft.AspNetCore.Mvc;
using NoThingStore.Models;
using NoThingStore.Services.Interfaces;
using System.Security.Claims;

namespace NoThingStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }

        public async Task<IActionResult> ManageOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return View(orders);
        }


        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetOrderByIdAsync(id.Value);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,OrderDate,Email")] Order order)
        {
            if (ModelState.IsValid)
            {
                await _orderService.CreateOrderAsync(order);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetOrderByIdAsync(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,OrderDate,Email,OrderItems")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            try
            {
                await _orderService.UpdateOrderAsync(order);
            }
            catch
            {
                if (!await _orderService.OrderExists(order.Id))
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

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetOrderByIdAsync(id.Value);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            await _orderService.DeleteOrderAsync(order);

            return RedirectToAction(nameof(Index));
        }

        // GET: Orders/AddOrderItem/5
        public async Task<IActionResult> AddOrderItem(int? orderId)
        {
            if (orderId == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetOrderByIdAsync(orderId.Value);
            if (order == null)
            {
                return NotFound();
            }

            // Create a new OrderItem and associate it with the order
            var orderItem = new OrderItem
            {
                OrderId = orderId.Value,
                Order = order
            };

            return View(orderItem);
        }

        // POST: Orders/AddOrderItem/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrderItem(int orderId, [Bind("Quantity,Name,Price,ProductId")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                var order = await _orderService.GetOrderByIdAsync(orderId);
                if (order == null)
                {
                    return NotFound();
                }

                orderItem.Order = order;
                order.OrderItems.Add(orderItem);

                await _orderService.UpdateOrderAsync(order);

                return RedirectToAction(nameof(Edit), new { id = orderId });
            }

            return View(orderItem);
        }

        // GET: Orders/RemoveOrderItem/5?orderId=1
        public async Task<IActionResult> RemoveOrderItem(int? orderId, int? orderItemId)
        {
            if (orderId == null || orderItemId == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetOrderByIdAsync(orderId.Value);
            if (order == null)
            {
                return NotFound();
            }

            var orderItem = order.OrderItems.FirstOrDefault(oi => oi.Id == orderItemId.Value);
            if (orderItem == null)
            {
                return NotFound();
            }

            order.OrderItems.Remove(orderItem);
            await _orderService.UpdateOrderAsync(order);

            return RedirectToAction(nameof(Edit), new { id = orderId });
        }
    }
}
