using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MVC_Master.Services;
using System.Security.Claims;

namespace MVC_Master.Controllers
{
	public class BaseController : Controller
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			
			ViewBag.ProjectName = "MVC-TEST";
			if (String.IsNullOrEmpty(User.Identity.Name))
			{
				ViewBag.Name = "Guest";
			}
			else
			{
				ViewBag.Name = User.Identity.Name;
			}

			ViewBag.Role = IdentityExtentions.GetRole(HttpContext.User);
			ViewBag.Token = IdentityExtentions.GetToken(HttpContext.User);

			base.OnActionExecuting(context);
		}

		public override void OnActionExecuted(ActionExecutedContext context)
		{
			base.OnActionExecuted(context);
		}
	}
}
