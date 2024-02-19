using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace MVC_Master.Services
{
	public static class IdentityExtentions
	{
		public static string GetToken(this ClaimsPrincipal user)
		{
			return user.FindFirst("Token")?.Value;
		}
		public static string GetRole(this ClaimsPrincipal user)
		{
			return user.FindFirst(ClaimTypes.Role)?.Value;
		}
	}
}