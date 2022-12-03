using Microsoft.AspNetCore.Identity;
using Skinet.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Infrastructure.Identity
{
	public class AppIdentityDBContextSeed
	{
		public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
		{

			if (!userManager.Users.Any())
			{

				var user = new AppUser()
				{
					DisplayName = "Bob",
					Email = "bob@test.com",
					UserName = "bob@test.com",
					Address = new Address()
					{
						FirstName = "Bob",
						LastName = "Bobbity",
						Street = "10 The street",
						State = "NY",
						ZipCode = "90210"
					}
				};

				await userManager.CreateAsync(user, "Pa$$w0rd");
			}


		}
	}
}
