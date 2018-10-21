using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Darknet.Models;
using Darknet.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Darknet.Web.Controllers
{
    public class AccountController : Controller
    {
        IHttpHelper _httpHelper;
        ConfigOptions _configOptions;
        DIStore _diStore;
        public AccountController(IHttpHelper httpHelper, IOptions<ConfigOptions> configOptions, DIStore diStore) {
            _httpHelper = httpHelper;
            _configOptions = configOptions.Value;
            _diStore = diStore;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterUser() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser([Bind] UserRegistrationModel userRegistrationModel) {
            if (ModelState.IsValid)
            {
                string uri = $"{_configOptions.ApiBaseUrl}/api/AccountApi/RegisterUser";
                string RegistrationStatus = await _httpHelper.PostAsync<UserRegistrationModel, string>(uri, userRegistrationModel);
                if (RegistrationStatus == "success")
                {
                    ModelState.Clear();
                    TempData["success"] = "Registration Successful!";
                    return View();
                }
                else
                {
                    TempData["failure"] = "This User ID already exists. Registration Failed.";
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            string IdpUrlWithReturnUrl = $"{ _configOptions.IdpLoginUrl}?returnUrl={_configOptions.WebBaseUrl}/Account/Session";
            return Redirect(IdpUrlWithReturnUrl);
            // return View();
        }
        [HttpPost]
        public async Task<IActionResult> Session([FromForm] string token) {
            _diStore.token = token;
            ClaimsPrincipal principal = ProcessToken(token);
            await HttpContext.SignInAsync(principal);
            return RedirectToAction("Index", "Home");
        }
        private ClaimsPrincipal ProcessToken(string token) {
            var signingKey = Encoding.ASCII.GetBytes(_configOptions.SigningKey);
            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out validatedToken);
            return principal;
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind] UserCredentialsModel userCredentialsModel) {
            if (ModelState.IsValid)
            {
                string uri = $"{_configOptions.ApiBaseUrl}/api/AccountApi/AuthenticateUser";
                string LoginStatus = await _httpHelper.PostAsync<UserCredentialsModel, string>(uri, userCredentialsModel);

                if (LoginStatus == "success")
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userCredentialsModel.Username)
                    };
                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(principal);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["LoginFailure"] = "Login Failed.Please enter correct credentials";
                    return View();
                }
            }
            else
                return View();

        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult Forbidden()
        {
            return View();
        }
    }
}