using BlogAppUI.ApiServices.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppUI.ViewComponents
{
    public class CategoryComponent:ViewComponent
    {
        private readonly ICategoryApiService _categoryApiService;
        public CategoryComponent(ICategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }
        public  IViewComponentResult Invoke()
        {
            var categories = _categoryApiService.GetCategoryWithBlogs().Result;
            return  View(categories);
        }
    }
}
