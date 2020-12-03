using BlogAppUI.ApiServices.Abstract;
using BlogAppUI.Extentions;
using BlogAppUI.Models;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BlogApiManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _httpClient.BaseAddress = new Uri("http://localhost:59229/api/blogs");
        }
        public async Task<List<BlogListModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<List<BlogListModel>>(await response.Content.ReadAsStringAsync());
                return result;
            }
            return null;
        }

        public async Task<List<BlogListModel>> GetAllWithCategoryIdAsync(int? id)
        {
            var response = await _httpClient.GetAsync("http://localhost:59229/api/blogs/GetAllWithCategoryId/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<List<BlogListModel>>(await response.Content.ReadAsStringAsync());
                return result;
            }
            return null;
        }

        public async Task<BlogListModel> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"blogs/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<BlogListModel>(await response.Content.ReadAsStringAsync());
                return result;
            }
            return null;
        }
        public async Task AddAsync(BlogAddModel model)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();
            if (model.Image != null)
            {
                var bytes = await System.IO.File.ReadAllBytesAsync(model.Image.FileName);
                ByteArrayContent byteArrayContent = new ByteArrayContent(bytes);
                byteArrayContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(model.Image.ContentType);
                formData.Add(byteArrayContent, nameof(model.Image.Name), model.Image.FileName);

            }
            var user = _httpContextAccessor.HttpContext.Session.GetObject<AppUserViewModel>("activeUser");
            model.AppUserId = user.Id;
            formData.Add(new StringContent(model.AppUserId.ToString()), nameof(model.AppUserId));
            formData.Add(new StringContent(model.ShortDescription), nameof(model.ShortDescription));
            formData.Add(new StringContent(model.Description), nameof(model.Description));
            formData.Add(new StringContent(model.Title), nameof(model.Title));
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            await _httpClient.PostAsync("", formData);


        }
    }
}
