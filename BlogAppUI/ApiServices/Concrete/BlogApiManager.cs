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
    public class BlogApiManager : IBlogApiService
    {
        private readonly HttpClient _httpClient;
        public BlogApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:59229/api/blogs");
        }
        public async Task<List<BlogListModel>> GetAllAsync()
        {
           var response =  await _httpClient.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
               var result =  JsonConvert.DeserializeObject<List<BlogListModel>>(await response.Content.ReadAsStringAsync());
                return result;
            }
            return null;
        }

        public async Task<BlogListModel> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"blogs/{id}");
            var imageResponse = await _httpClient.GetAsync($"/Images/GetBlogImageById/{id}");
            if (response.IsSuccessStatusCode&&imageResponse.IsSuccessStatusCode)
            {
                var image = JsonConvert.DeserializeObject<BlogListModel>(await response.Content.ReadAsStringAsync());
                var result = JsonConvert.DeserializeObject<BlogListModel>(await response.Content.ReadAsStringAsync());
                return result;
            }
            return null;
        }
    }
}
