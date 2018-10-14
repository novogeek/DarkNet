using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Darknet.Models;
using Darknet.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;


namespace Darknet.Web.Controllers
{
    public class AccountController : Controller
    {
        IHttpHelper _httpHelper;
        public AccountController(IHttpHelper httpHelper) {
            _httpHelper = httpHelper;
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
                string uri = "https://localhost:44346/api/Account/RegisterUser";
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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([Bind] UserCredentialsModel userCredentialsModel) {
            if (ModelState.IsValid)
            {
                string uri = "https://localhost:44346/api/Account/AuthenticateUser";
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