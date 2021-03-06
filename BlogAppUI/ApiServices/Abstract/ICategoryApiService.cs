﻿using BlogAppUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppUI.ApiServices.Abstract
{
    public interface ICategoryApiService
    {
        Task<List<CategoryListModel>> GetAll();
        Task<List<CategoryWithBlogsViewModel>> GetCategoryWithBlogs();
    }
}
