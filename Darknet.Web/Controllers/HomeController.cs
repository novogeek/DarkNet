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
using Microsoft.AspNetCore.Http;

namespace Darknet.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        IHttpHelper _httpHelper;
        ConfigOptions _configOptions;
        //DIStore _diStore;
        public HomeController(IHttpHelper httpHelper, IOptions<ConfigOptions> configOptions, DIStore diStore)
        {
            _httpHelper = httpHelper;
            _configOptions = configOptions.Value;
            _httpHelper.AddBearerToken(diStore.token);
        }
        public async Task<IActionResult> Index(string username)
        {
            string targetUser = username;
            string loggedInUser = User.Identity.Name;
            string postsUri = "";
            string userDetailsUri = "";
            string privacyLevelsUri = "";

            // If the endpoint is accessed in user profile mode (i.e., the url has the querystring as /Home/Profile?username=..), pickup username param
            if (!String.IsNullOrEmpty(targetUser))
            {
                userDetailsUri = $"{_configOptions.ApiBaseUrl}/api/UserDetailsApi/GetUserDetails?username={targetUser}";
                postsUri = $"{_configOptions.ApiBaseUrl}/api/UserDetailsApi/GetPostsOfTargetUser?targetUser={targetUser}";
            }
            // If the endpoint is accessed as home page (i.e., /Home/Index), pick up loggedInUser from JWT in the API
            else
            {
                userDetailsUri = $"{_configOptions.ApiBaseUrl}/api/UserDetailsApi/GetUserDetails";
                postsUri = $"{_configOptions.ApiBaseUrl}/api/UserDetailsApi/GetAllPermissiblePosts";
            }

            privacyLevelsUri = $"{_configOptions.ApiBaseUrl}/api/UserDetailsApi/GetPrivacyLevels";

            UserDetailsModel userDetailsModel = await _httpHelper.GetAsync<UserDetailsModel>(userDetailsUri);
            List<UserPostsModel> lstUserPostsModel = await _httpHelper.GetAsync<List<UserPostsModel>>(postsUri);
            List<PrivacyLevelsModel> lstPrivacyLevelsModel = await _httpHelper.GetAsync<List<PrivacyLevelsModel>>(privacyLevelsUri);

            UserDetailsViewModel userDetailsViewModel = new UserDetailsViewModel
            {
                FirstName = userDetailsModel.FirstName,
                LastName = userDetailsModel.LastName,
                Address = userDetailsModel.Address,
                Mobile = userDetailsModel.Mobile,
                FriendsListDict = userDetailsModel.Friends
                    .OrderByDescending(d => d.PrivacyLevel)
                    .ThenBy(b => b.FirstName)
                    .GroupBy(f => f.PrivacyLevel)
                    .ToDictionary(g => g.Key, g => g.ToList()),
                lstPrivacyLevelsModel = lstPrivacyLevelsModel,
                lstUserPostsModels = lstUserPostsModel,
                lstFriends = userDetailsModel.Friends
            };
            ViewData["ImpersonationIDPUrl"] = _configOptions.IdpImpersonationUrl;
            ViewData["ImpersonationRetUrl"] = $"returnUrl={_configOptions.WebBaseUrl}/Account/Implicit";
            ViewData["ApiBaseUrl"] = _configOptions.ApiBaseUrl;
            return View(userDetailsViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> StatusUpdate([FromForm] AddPostModel addPostModel) {
            AddPostViewModel addPostViewModel = new AddPostViewModel() {
                post = addPostModel.post,
                privacy = addPostModel.privacy
            };
            string uri = $"{_configOptions.ApiBaseUrl}/api/UserDetailsApi/StatusUpdate";
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
