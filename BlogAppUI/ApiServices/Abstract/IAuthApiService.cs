using BlogAppUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppUI.ApiServices.Abstract
{
    public interface IAuthApiService
    {
        Task<bool> SignInAsync(AppUserSignInModel model);
    }
}
