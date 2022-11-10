using Microsoft.EntityFrameworkCore;
using Skinet.Core;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Skinet.Infrastructure
{
	public static class SkinetContextSeedData 
	{

		public static async Task SeedData(SkinetContext context) {

			if (!context.Brands.Any()) { 
				


			}
		} 

	}
}
