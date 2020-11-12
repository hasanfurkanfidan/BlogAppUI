using BlogAppUI.ApiServices.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlogAppUI.ApiServices.Concrete
{
    public class ImageApiManager:IImageApiService
    {
        private readonly HttpClient _httpClient;
        public ImageApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:59229/api/Images/");
        }
        public async Task<string>GetImageWithBlogString(int id)
        {
            var response =await _httpClient.GetAsync($"GetBlogImageById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result =await response.Content.ReadAsByteArrayAsync();
                return $"data:image/jpeg;base64,{Convert.ToBase64String(result)}";
            }
            return null;
        }
    }
}
