﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoThingStore.Services.Interfaces;
using System.Security.Claims;

namespace NoThingStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, IdentityUser editedUser)
        {

            var dbUser = await _userService.GetUserByIdAsync(id);
            if (dbUser == null)
            {
                return NotFound();
            }

            dbUser.Email = editedUser.Email;
            dbUser.UserName = editedUser.Email;

            await _userService.UpdateUserAsync(dbUser);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, IdentityUser user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var dbUser = await _userService.GetUserByIdAsync(id);
            if (dbUser == null)
            {
                return NotFound();
            }

            await _userService.DeleteUserAsync(dbUser);

            return RedirectToAction(nameof(Index));
        }
    }
}
