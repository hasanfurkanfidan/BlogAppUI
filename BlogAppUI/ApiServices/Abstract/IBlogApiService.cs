using BlogAppUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppUI.ApiServices.Abstract
{
    public interface IBlogApiService
    {
        Task<List<BlogListModel>> GetAllAsync();
        Task<BlogListModel> GetByIdAsync(int id);
        Task<List<BlogListModel>> GetAllWithCategoryIdAsync(int? id);
        Task AddAsync(BlogAddModel model);

        Task UpdateAsync(BlogUpdateModel model);
        Task DeleteAsync(int id);
    }
}
