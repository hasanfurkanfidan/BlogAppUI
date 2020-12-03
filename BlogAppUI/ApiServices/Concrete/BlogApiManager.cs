using BlogAppUI.ApiServices.Abstract;
using BlogAppUI.Extentions;
using BlogAppUI.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
            _httpClient.BaseAddress = new Uri("http://localhost:59229/api/blogs/");
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
            var response = await _httpClient.GetAsync("http://localhost:59229/api/blogs/GetAllWithCategoryId" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<List<BlogListModel>>(await response.Content.ReadAsStringAsync());
                return result;
            }
            return null;
        }

        public async Task<BlogListModel> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($""+id.ToString());

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
            //image control
            if (model.Image != null)
            {
                //Designing byte array for form's image
                //var bytes = await System.IO.File.ReadAllBytesAsync(model.Image.FileName);
                //Set the content
                var stream = new MemoryStream();
                await model.Image.CopyToAsync(stream);
                var bytes = stream.ToArray();
                ByteArrayContent byteArrayContent = new ByteArrayContent(bytes);
                byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                //add bytearraycontent to formdata
                formData.Add(byteArrayContent, nameof(BlogAddModel.Image), model.Image.FileName);

            }
            //Get the user from session
            var user = _httpContextAccessor.HttpContext.Session.GetObject<AppUserViewModel>("activeUser");
            model.AppUserId = user.Id;

            //adding formdata from other components
            formData.Add(content: new StringContent(model.AppUserId.ToString()), nameof(BlogAddModel.AppUserId));
            formData.Add(content: new StringContent(model.Title), nameof(BlogAddModel.Title));
            formData.Add(content: new StringContent(model.Description), nameof(BlogAddModel.Description));
            formData.Add(content: new StringContent(model.ShortDescription), nameof(BlogAddModel.ShortDescription));

            //Check authorization
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));
            //check the response 
            var response = await _httpClient.PostAsync("", formData);
            if (response.IsSuccessStatusCode)
            {

            }

        }
        public async Task DeleteAsync(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));
            var response = await _httpClient.DeleteAsync($"" + id.ToString());
            if (response.IsSuccessStatusCode)
            {

            }

        }
        public async Task UpdateAsync(BlogUpdateModel model)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();
            if (model.Image != null)
            {
                //Set Stream
                var stream = new MemoryStream();
                await model.Image.CopyToAsync(stream);
                var bytes = stream.ToArray();
                ByteArrayContent byteArrayContent = new ByteArrayContent(bytes);
                byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                formData.Add(byteArrayContent, nameof(BlogUpdateModel.Image), model.Image.FileName);
            }
            var user = _httpContextAccessor.HttpContext.Session.GetObject<AppUserViewModel>("activeUser");
            model.AppUserId = user.Id;
            formData.Add(new StringContent(model.AppUserId.ToString()), nameof(BlogUpdateModel.AppUserId));
            formData.Add(content: new StringContent(model.Title), nameof(BlogUpdateModel.Title));
            formData.Add(content: new StringContent(model.ShortDescription), nameof(BlogUpdateModel.ShortDescription));
            formData.Add(content: new StringContent(model.Description), nameof(BlogUpdateModel.Description));
            formData.Add(content: new StringContent(model.Id.ToString()), nameof(BlogUpdateModel.Id));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));
            var response = _httpClient.PutAsync($"" + model.Id, formData);

        }
       
    }
}
