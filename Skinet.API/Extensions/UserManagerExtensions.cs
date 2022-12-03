using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Skinet.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Skinet.API.Extensions
{
	public static class UserManagerExtensions
	{
		public static async Task<AppUser> FindUserByClaimsPrincipalWithAddressAsync(this UserManager<AppUser> input, ClaimsPrincipal user)
		{
			var email = user.FindFirstValue(ClaimTypes.Email);
			return await input.Users.Include(user => user.Address)
				.SingleOrDefaultAsync(user => user.Email == email);
		}

		public static async Task<AppUser> FindByEmailFromClaimsPrincipal(this UserManager<AppUser> input, ClaimsPrincipal user)
		{
			var email = user.FindFirstValue(ClaimTypes.Email);
			return await input.Users.SingleOrDefaultAsync(user => user.Email == email);
		}
	}
}
