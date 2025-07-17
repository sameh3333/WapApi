using Azure;
using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Ui.Models;
using Ui.Services;
namespace Ui.Controllers
{

    public class AccountController : Controller
    {

        IUserService _userServices;
        private readonly GenericApiClient _apiClient;
        public AccountController(IUserService userServices, GenericApiClient apiClient)
        {
            _userServices = userServices;
            _apiClient=apiClient;   
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(); 
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDtos user)
        {
            try
            {
                if(!ModelState.IsValid)
                    return View(user);
                var result = await _userServices.LoginAsync(user);
                if (result.Success)
                {
                    LoginApiModel apiResulte = await _apiClient.PostAsync<LoginApiModel>("api/auth/login", user);
                    if (apiResulte == null)
                    {
                        ModelState.AddModelError(String.Empty, "Api error:Undle to Process Login .");
                        return View(user);
                    }
                    var accesstoken = apiResulte?.AccessToken.ToString();
                    if (string.IsNullOrEmpty(accesstoken))
                    {
                        ModelState.AddModelError(String.Empty, "Invalid Login Attampt ");
                        return View(user);
                    }
                    // حفظ التوكن في الكوكيز
                    Response.Cookies.Append("AccessToken", accesstoken, new CookieOptions
                    {
                        HttpOnly = false,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddHours(1)
                    });
                    Response.Cookies.Append("RefreshToken", apiResulte?.RefeshToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddDays(7)
                    });
                    var dbuser=await _userServices.GetUserByEmailAsync(user.Email);
                    if (dbuser.Role.ToLower() == "admin") 
                        return RedirectToAction("Index", "Home", new { area = "admin" });
                    else
                        return RedirectToAction("Index", "Home");

                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                // سجّل أو أعد رمي الخطأ مع التفاصيل
                throw new Exception($"Login error: {ex.Message}", ex);
            }
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View(); // لازم برضو تعمل AccessDenied.cshtml
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Register(UserDto registerDto)
       {
            if (!ModelState.IsValid)
                return View(registerDto);

            var result = await _userServices.RegisterAsync(registerDto);

            if (!result.Success)
                  return View ("Register", registerDto);

                //  return View (new { errors = result.Errors });

                return RedirectToAction("Login", "Account" );
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
