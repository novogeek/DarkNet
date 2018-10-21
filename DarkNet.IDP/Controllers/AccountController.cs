using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Darknet.IDP.Models;
using Darknet.Models;
using Darknet.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Darknet.Web.Controllers
{
    public class AccountController : Controller
    {
        IdpConfigOptions _configOptions;
        IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository, IOptions<IdpConfigOptions> configOptions) {
            _accountRepository = accountRepository;
            _configOptions = configOptions.Value;
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
        public IActionResult RegisterUser([Bind] UserRegistrationModel userRegistrationModel) {
            if (ModelState.IsValid)
            {
                string statusMsg = "";
                statusMsg = _accountRepository.RegisterUser(userRegistrationModel);

                if (statusMsg == "success")
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
        public async Task<IActionResult> Login([Bind] UserCredentialsModel userCredentialsModel, string returnUrl = "") {
            if (ModelState.IsValid)
            {
                string statusMsg = "";
                statusMsg = _accountRepository.AuthenticateUser(userCredentialsModel);

                if (statusMsg == "success")
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userCredentialsModel.Username)
                    };
                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(principal);
                    HttpContext.Session.SetString("token", "testToken");
                    HttpContext.Session.SetString("returnUrl", String.IsNullOrEmpty(returnUrl)?"":returnUrl);
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