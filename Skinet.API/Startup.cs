using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skinet.Infrastructure;
using Core.Interfaces;
using Skinet.API.Helpers;
using Skinet.API.Middleware;
using Skinet.API.Errors;
using Skinet.API.Extensions;
using StackExchange.Redis;
using Skinet.Infrastructure.Identity;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Skinet.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllers();
			services.AddDbContext<SkinetContext>(x => x.UseNpgsql(Configuration.GetConnectionString("SkinetContext")));
			services.AddDbContext<AppIdentityDbContext>(x => x.UseNpgsql(Configuration.GetConnectionString("AppIdentityContext")));
			services.AddSingleton<IConnectionMultiplexer>(c =>
			{
				var configuration = ConfigurationOptions.Parse(Configuration.GetConnectionString("Redis"), true);
				return ConnectionMultiplexer.Connect(configuration);
			});
			services.AddApplicationServices();
			services.AddIdentityServices(Configuration); 
			services.AddSwaggerDocumentation(); 
			services.AddAutoMapper(typeof(MappingProfiles));
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", policy =>
				{
					policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
				});
			});
			

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			// this is for exceptions (ex. null reference)
			app.UseMiddleware<ExceptionMiddleware>();
			app.UseSwaggerDocumentation(); 

			app.UseStatusCodePagesWithReExecute("/errors/{0}"); // this is for error codes that aren't explicitly specified in our controllers like 404 error code for non existant pages/Urls
			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseStaticFiles();
			app.UseStaticFiles(new StaticFileOptions() {
				FileProvider = new PhysicalFileProvider(
				Path.Combine(Directory.GetCurrentDirectory(), "Content")),
				RequestPath = "/content"
			});

			app.UseCors("CorsPolicy");

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapFallbackToController("Index", "Fallback");
			});
		}
	}
}
