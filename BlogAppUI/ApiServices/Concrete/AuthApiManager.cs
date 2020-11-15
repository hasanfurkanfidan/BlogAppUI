using BlogAppUI.ApiServices.Abstract;
using BlogAppUI.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlogAppUI.ApiServices.Concrete
{
    public class AuthApiManager : IAuthApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpAccessor;
        public AuthApiManager(HttpClient httpClient,IHttpContextAccessor httpAccessor)
        {
            _httpClient = httpClient;
            _httpAccessor = httpAccessor;
            _httpClient.BaseAddress = new Uri("http://localhost:59229/api/auth/");
        }
        public async Task<bool> SignInAsync(AppUserSignInModel model)
        {
            var data = JsonConvert.SerializeObject(model);
            var content = new StringContent(data,Encoding.UTF8,"application/json");
            var response =await _httpClient.PostAsync("SignIn",content);
            if (response.IsSuccessStatusCode)
            {
                var token = JsonConvert.DeserializeObject<AccessTokenModel>(await response.Content.ReadAsStringAsync());
                _httpAccessor.HttpContext.Session.SetString("token", token.Token);
                return true;
            }
            return false;
        }

     
    }
}
