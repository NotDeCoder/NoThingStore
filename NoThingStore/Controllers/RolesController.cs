using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace NoThingStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        public async Task<IActionResult> ManageUsers(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            
            // Get IdentityUsers in this role
            var users = new List<IdentityUser>();
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    users.Add(user);
                }
            }

            // Pass role and users to view
            ViewBag.Role = role;

            return View("~/Views/Users/Index.cshtml", users);
        }

        public async Task<IActionResult> AddUserToRole(string userId, string roleId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByIdAsync(roleId);

            await _userManager.AddToRoleAsync(user, role.Name);

            return RedirectToAction("Edit", "Users", new { id = userId });
        }

        public async Task<IActionResult> RemoveUserFromRole(string userId, string roleId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByIdAsync(roleId);

            await _userManager.RemoveFromRoleAsync(user, role.Name);

            return RedirectToAction("Edit", "Users", new { id = userId });
        }
    }
}
