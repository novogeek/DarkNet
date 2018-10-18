using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Darknet.Utilities;
using Microsoft.AspNetCore.Mvc;
using Darknet.Models;
using Microsoft.Extensions.Options;
using Darknet.Web.Models;

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
        public async Task<IActionResult> Profile(string username)
        {
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
                    .OrderByDescending(d => d.PrivacyLevel)
                    .ThenBy(b => b.FirstName)
                    .GroupBy(f => f.PrivacyLevel)
                    .ToDictionary(g => g.Key, g => g.ToList()),
                lstPrivacyLevelsModel = lstPrivacyLevelsModel
            };
            return View(userDetailsViewModel);
        }

        //public async Task<JsonResult> GetPrivacyLevels()
        //{
        //    string plUri = $"{_configOptions.BaseUrl}/api/UserDetails/GetPrivacyLevels";
        //    PrivacyLevelsModel privacyLevelsModel = await _httpHelper.GetAsync<PrivacyLevelsModel>(plUri);
            
        //}
    }
}
