using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Darknet.Utilities;
using Microsoft.AspNetCore.Mvc;
using Darknet.Models;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Darknet.Web.Controllers
{
    public class UserDetailsController : Controller
    {
        IHttpHelper _httpHelper;
        ConfigOptions _configOptions;
        public UserDetailsController(IHttpHelper httpHelper, IOptions<ConfigOptions> configOptions)
        {
            _httpHelper = httpHelper;
            _configOptions = configOptions.Value;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Profile()
        {
            string username = User.Identity.Name;
            string uri = $"{_configOptions.BaseUrl}/api/UserDetails/GetUserDetails?username={username}";
            UserDetailsModel userDetailsModel = await _httpHelper.GetAsync<UserDetailsModel>(uri);
            return View(userDetailsModel);
        }
    }
}
