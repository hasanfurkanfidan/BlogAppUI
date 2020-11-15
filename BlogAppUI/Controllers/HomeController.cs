using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAppUI.ApiServices.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogAppUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogApiService _blogApiService;
        public HomeController(IBlogApiService blogApiService)
        {
            _blogApiService = blogApiService;
        }
        public async Task< IActionResult> Index(int? categoryId)
        {
            if (categoryId.HasValue)            
            {
                var blogs = await _blogApiService.GetAllWithCategoryIdAsync(categoryId);
                ViewBag.ActiveCategory = categoryId;
                return View(blogs);
            }
            else
            {
                var blogs = await _blogApiService.GetAllAsync();
                return View(blogs);
            }
          
        }
        public async Task<IActionResult>Detail(int id)
        {
            var blog = await _blogApiService.GetByIdAsync(id);
            return View(blog);
        }
       
    }
}
