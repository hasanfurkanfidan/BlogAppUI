using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAppUI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogAppUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class HomeController : Controller
    {
        [JwtAuthorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
