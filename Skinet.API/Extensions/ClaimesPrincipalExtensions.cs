using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Skinet.API.Extensions
{
	public static class ClaimesPrincipalExtensions
	{
		public static string RetreiveEmailFromPrincipal(this ClaimsPrincipal user)
		{
			return user.FindFirstValue(ClaimTypes.Email);
		}
	}
}
