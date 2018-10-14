using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Darknet.Utilities;
using Microsoft.AspNetCore.Mvc;
using Darknet.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Darknet.Web.Controllers
{
    public class UserDetailsController : Controller
    {
        IHttpHelper _httpHelper;
        public UserDetailsController(IHttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Profile()
        {
            string username = User.Identity.Name;
            string uri = "https://localhost:44346/api/UserDetails/GetUserDetails?username=" + username;
            UserDetailsModel userDetailsModel = await _httpHelper.GetAsync<UserDetailsModel>(uri);
            return View(userDetailsModel);
        }
    }
}
