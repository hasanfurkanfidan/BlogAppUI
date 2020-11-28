using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAppUI.ApiServices.Abstract;
using BlogAppUI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BlogAppUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [JwtAuthorize]
    public class BlogController : Controller
    {
        private readonly IBlogApiService _blogApiService;
        public BlogController(IBlogApiService blogApiService)
        {
            _blogApiService = blogApiService;
        }
        public async Task< IActionResult> Index()
        {
            var blogs =await _blogApiService.GetAllAsync();
            return View(blogs);
        }
    }
}
