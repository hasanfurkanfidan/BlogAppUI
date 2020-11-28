using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAppUI.ApiServices.Abstract;
using BlogAppUI.Filters;
using BlogAppUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogAppUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthApiService _authApiService;
        public AccountController(IAuthApiService authApiService)
        {
            _authApiService = authApiService;
        }
        public  IActionResult SignIn()
        {
            var model = new AppUserSignInModel();
            return View(model);
        }
        
        
        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserSignInModel model)
        {
            if (await _authApiService.SignInAsync(model))
            {
                return RedirectToAction("Index", "Home", new { @area = "Admin" });
            }
            return View();
        } 
    }
}
