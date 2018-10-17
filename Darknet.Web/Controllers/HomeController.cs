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
using Darknet.Web.Models;
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

        public async Task<IActionResult> Index()
        {
            string username = User.Identity.Name;
            string uri = $"{_configOptions.BaseUrl}/api/UserDetails/GetUserDetails?username={username}";
            UserDetailsModel userDetailsModel = await _httpHelper.GetAsync<UserDetailsModel>(uri);

            string plUri = $"{_configOptions.BaseUrl}/api/UserDetails/GetPrivacyLevels";
            List<PrivacyLevelsModel> lstPrivacyLevelsModel = await _httpHelper.GetAsync<List<PrivacyLevelsModel>>(plUri);

            UserDetailsViewModel userDetailsViewModel = new UserDetailsViewModel
            {
                FirstName = userDetailsModel.FirstName,
                LastName = userDetailsModel.LastName,
                Address = userDetailsModel.Address,
                Mobile = userDetailsModel.Mobile,
                FriendsListDict = userDetailsModel.Friends
                    .OrderByDescending(d=>d.PrivacyLevel)
                    .OrderBy(b => b.FirstName)
                    .GroupBy(f => f.PrivacyLevel)
                    .ToDictionary(g => g.Key, g => g.ToList()),
                lstPrivacyLevelsModel = lstPrivacyLevelsModel
            };
            return View(userDetailsViewModel);
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
