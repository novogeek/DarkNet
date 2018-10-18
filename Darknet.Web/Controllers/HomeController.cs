using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Darknet.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Darknet.Utilities;
using Darknet.Models;
using Microsoft.Extensions.Options;

namespace Darknet.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        IHttpHelper _httpHelper;
        ConfigOptions _configOptions;
        public HomeController(IHttpHelper httpHelper, IOptions<ConfigOptions> configOptions)
        {
            _httpHelper = httpHelper;
            _configOptions = configOptions.Value;
        }
        public async Task<IActionResult> Index(string username)
        {
            string targetUser = username;
            string loggedInUser = User.Identity.Name;
            string uri = $"{_configOptions.BaseUrl}/api/UserDetails/GetUserDetails?loggedInUser={loggedInUser}&targetUser={targetUser}";

            UserDetailsViewModel userDetailsViewModel = await _httpHelper.GetAsync<UserDetailsViewModel>(uri);
            
            return View(userDetailsViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> StatusUpdate([FromForm] AddPostModel addPostModel) {
            AddPostViewModel addPostViewModel = new AddPostViewModel() {
                post = addPostModel.post,
                privacy = addPostModel.privacy,
                username = User.Identity.Name
            };
            string uri = $"{_configOptions.BaseUrl}/api/UserDetails/StatusUpdate";
            string RegistrationStatus = await _httpHelper.PostAsync<AddPostViewModel, string>(uri, addPostViewModel);
            return RedirectToAction("Index");
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
