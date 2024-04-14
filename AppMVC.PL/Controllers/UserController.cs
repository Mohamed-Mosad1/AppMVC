using AppMVC.DAL.Models;
using AppMVC.PL.Helpers;
using AppMVC.PL.ViewModels;
using AppMVC.PL.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppMVC.PL.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index(string searchInput)
        {
            var users = Enumerable.Empty<UserViewModel>();
            if (string.IsNullOrEmpty(searchInput))
            {
                users = await _userManager.Users.Select(U => new UserViewModel()
                {
                    Id = U.Id,
                    FName = U.FName,
                    LName = U.LName,
                    UserEmail = U.Email,
                    Roles = _userManager.GetRolesAsync(U).Result

                }).ToListAsync();
            }
            else
            {
                users = await _userManager.Users.Where(U => U.Email.ToLower().Contains(searchInput.ToLower()))
                                                .Select(U => new UserViewModel()
                                                {
                                                    Id = U.Id,
                                                    FName = U.FName,
                                                    LName = U.LName,
                                                    UserEmail = U.Email,
                                                    Roles = _userManager.GetRolesAsync(U).Result
                                                }).ToListAsync();
            }

            return View(users);
        }

        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null)
            {
                return BadRequest(); // 400
            }

            var userFromDb = await _userManager.FindByIdAsync(id);

            if (userFromDb is null)
                return NotFound(); // 404

            var user = new UserViewModel()
            {
                Id = userFromDb.Id,
                FName = userFromDb.FName,
                LName = userFromDb.LName,
                UserEmail = userFromDb.Email,
                Roles = _userManager.GetRolesAsync(userFromDb).Result
            };

            return View(viewName, user);
        }

        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel userViewModel) 
        {
            if (id != userViewModel.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var userFromDb = await _userManager.FindByIdAsync(id);
                if (userFromDb is null)
                    return NotFound();

                userFromDb.FName = userViewModel.FName;
                userFromDb.LName = userViewModel.LName;
                userFromDb.Email = userViewModel.UserEmail;

                await _userManager.UpdateAsync(userFromDb);
                return RedirectToAction(nameof(Index)); 
            }

            return View(userViewModel);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] string id , UserViewModel userViewModel)
        {
            if(id != userViewModel.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var userFromDb = await _userManager.FindByIdAsync(id);
                if(userFromDb is null)
                    return NotFound();

                await _userManager.DeleteAsync(userFromDb);

                return RedirectToAction(nameof(Index));
            }

            return View(userViewModel);
            
        }
    
    }
}
