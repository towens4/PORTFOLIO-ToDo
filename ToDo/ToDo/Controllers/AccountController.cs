﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDo.Interfaces;
using ToDo.Models.DataModels;
using ToDo.ViewModels;

namespace ToDo.Controllers
{
    public class AccountController : Controller
    {
        SignInManager<IdentityUser> _signInManager;
        UserManager<IdentityUser> _userManager;
        
        
        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            
        }
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = register.Email, Email = register.Email };
                var result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    HttpContext.Session.SetString("Id", user.Id.ToString());
                    return RedirectToAction("Index", "Assignment");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                
            }
                
            return View(register);

            
            
            
        }

        public IActionResult Login()
        {

            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            ModelState.Remove("AssignmentList");
            if (ModelState.IsValid)
            {
                var userTask = await _userManager.FindByNameAsync(login.Email);
                var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

                if (result.Succeeded)
                {
                    HttpContext.Session.SetString("Id", userTask.Id.ToString());
                    return RedirectToAction("Index", "Assignment");
                }
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

                return View();   
        }

        public IActionResult LogOut(AssignmentListModel assignmentListModel)
        {
            
            _signInManager.SignOutAsync();
            //assignmentListModel.AssignmentList.ToList().Clear();
            return RedirectToAction("LogIn", "Account");
        }
    }
}
