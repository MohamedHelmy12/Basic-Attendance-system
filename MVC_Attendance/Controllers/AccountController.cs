﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MVC_Attendance.IRepository;
using MVC_Attendance.Models;
using MVC_Attendance.ViewModels;
using System.Security.Claims;

namespace MVC_Attendance.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository accountRepository;
        public AccountController(IAccountRepository _accountRepository)
        {
            accountRepository = _accountRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login(UserLoginModelView userLogin)
        {
            if (userLogin != null)
            {
                if (ModelState.IsValid)
                {
                    var user = accountRepository.GetUserAuth(userLogin);
                    if (user != null)
                    {
                        var claimPrincipal = accountRepository.AddUserAuthentication(user);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal);
                        //await Console.Out.WriteLineAsync(User.FindFirst(ClaimTypes.Role)?.Value);
                        //var role = (User.FindFirst(ClaimTypes.Role)?.Value);
                        var role = user.Role.ToString();
                        if (role == "Student")
                            return RedirectToAction("Show", "Student");
                        else if (role == "Instructor")
                            return RedirectToAction("Index", "instructor");
                        else if (role == "Admin")
                            return RedirectToAction("Show", "Student");
                        else if (role == "Employee")
                            return RedirectToAction("Index", "Employee");
                        else
                            return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Invalid Email or Password");
                    }
                }
            }

            return View(userLogin);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

    }
}
