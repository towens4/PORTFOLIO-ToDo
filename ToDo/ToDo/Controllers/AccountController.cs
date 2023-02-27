using Microsoft.AspNetCore.Identity;
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
            return View(new AssignmentListModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(AssignmentListModel register)
        {
            if (!ModelState.IsValid)
                return View(register);

            var user = new IdentityUser { UserName = register.RegisterModel.Email, Email= register.RegisterModel.Email };
            var result = await _userManager.CreateAsync(user, register.RegisterModel.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            HttpContext.Session.SetString("Id", user.Id.ToString());
            
            return RedirectToAction("Index", "Assignment");
        }

        public IActionResult Login()
        {

            return View(new AssignmentListModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(AssignmentListModel login)
        {
            ModelState.Remove("AssignmentList");
            if (!ModelState.IsValid)
                return View();

            var userTask = await _userManager.FindByNameAsync(login.LoginModel.Email);
            var result = await _signInManager.PasswordSignInAsync(login.LoginModel.Email, login.LoginModel.Password, false, false);

            if (result.Succeeded)
            {
                HttpContext.Session.SetString("Id", userTask.Id.ToString());
                return RedirectToAction("Index", "Assignment");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                return View();
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
