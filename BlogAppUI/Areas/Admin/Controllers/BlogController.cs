using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAppUI.ApiServices.Abstract;
using BlogAppUI.Filters;
using BlogAppUI.Models;
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
        public async Task<IActionResult> Index()
        {
            var blogs = await _blogApiService.GetAllAsync();
            return View(blogs);
        }
        public IActionResult Create()
        {
            return View(new BlogAddModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(BlogAddModel model)
        {
            if (ModelState.IsValid)
            {
                await _blogApiService.AddAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _blogApiService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var blog = await _blogApiService.GetByIdAsync(id);
            var model = new BlogUpdateModel();
            model.Description = blog.Description;
            model.Id = blog.Id;
            model.ShortDescription = blog.ShortDescription;
            model.Title = blog.Description;
            model.Image = blog.Image;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(BlogUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                await _blogApiService.UpdateAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);

        }
    }
}
