using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoThingStore.Models;
using NoThingStore.Services.Interfaces;

namespace NoThingStore.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;

        public AdminController(IUserService userService, IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id, IdentityUser editedUser)
        {
            if (id != editedUser.Id)
            {
                return BadRequest();
            }

            var result = await _userService.UpdateUserAsync(editedUser);
            if (!result.Succeeded)
            {
                return View(editedUser);
            }

            return RedirectToAction(nameof(Users));
        }

        public async Task<IActionResult> Orders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }

        public async Task<IActionResult> EditOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> EditOrder(int id, Order editedOrder)
        {
            if (id != editedOrder.Id)
            {
                return BadRequest();
            }

            var result = _orderService.UpdateOrderAsync(editedOrder);
            if (!result.IsCompletedSuccessfully)
            {
                return View(editedOrder);
            }

            return RedirectToAction(nameof(Orders));
        }
    }
}
