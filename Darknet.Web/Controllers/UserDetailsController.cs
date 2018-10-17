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
            UserDetailsViewModel userDetailsViewModel = new UserDetailsViewModel
            {
                FirstName = userDetailsModel.FirstName,
                LastName = userDetailsModel.LastName,
                Address = userDetailsModel.Address,
                Mobile = userDetailsModel.Mobile,
                FriendsListDict = userDetailsModel.Friends.GroupBy(f => f.PrivacyLevel).ToDictionary(g => g.Key, g => g.ToList())
            };
            return View(userDetailsViewModel);
        }
    }
}
