using BlogAppUI.ApiServices.Abstract;
using BlogAppUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlogAppUI.ApiServices.Concrete
{
    public class CategoryApiManager : ICategoryApiService
    {
        private readonly HttpClient _httpClient;
        public CategoryApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:59229/api/categories");
        }

        public async Task<List<CategoryListModel>> GetAll()
        {
            var response = await _httpClient.GetAsync("");
            var countresponse = await _httpClient.GetAsync("http://localhost:59229/api/categories");
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<List<CategoryListModel>>(await response.Content.ReadAsStringAsync());
                return result;
            }
            return null;
        }

        public async Task<List<CategoryWithBlogsViewModel>> GetCategoryWithBlogs()
        {
            var response = await _httpClient.GetAsync("http://localhost:59229/api/categories/GetWithCategryCount");
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<List<CategoryWithBlogsViewModel>>(await response.Content.ReadAsStringAsync());
                return result;
            }
            return null;
        }
    }
}
