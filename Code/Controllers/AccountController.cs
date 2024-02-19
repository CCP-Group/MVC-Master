using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MVC_Master.Services;
using System.Security.Claims;

namespace MVC_Master.Controllers{
    public class AccountController : Controller
    {
		private readonly INotyfService _notyf;
        public AccountController(INotyfService notyfService)
        {
			_notyf = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login(string userName, string passWord)
        {
			bool IsAuth = false;
			ClaimsIdentity claim = null;

			// API-Login Project
			var login = await NetworkServices.CheckLoginMasterCCP(userName, passWord);

			// API-Token
			var token = await NetworkServices.LoginToken(userName, passWord);

			if (login.autoID != 0 && login.employeeID != null)
			{
				//Set claim
				claim = new ClaimsIdentity(new[] {
						new Claim(ClaimTypes.Name, $"{login.name } {login.surname}"),
						new Claim(ClaimTypes.Role, "Admin"),
						new Claim("Token",token)
					}, CookieAuthenticationDefaults.AuthenticationScheme);
				IsAuth = true;

				//Add Cookie
				if (IsAuth)
				{
					var principal = new ClaimsPrincipal(claim);
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
				}
				//Home
				_notyf.Success($"ยินดีต้อนรับคุณ {login.name} {login.surname}" );
				return RedirectToAction("Index", "Home");
			}
			_notyf.Error("ไม่พบข้อมูลที่ท่านกรอก");
			return RedirectToAction("Index");
        }

		public IActionResult ErrorForbidden()
		{
			return View();
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Account");
		}
    }
}
