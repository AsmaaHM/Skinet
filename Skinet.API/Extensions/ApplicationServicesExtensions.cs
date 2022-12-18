using Core.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Skinet.API.Errors;
using Skinet.Core.Interfaces;
using Skinet.Infrastructure.Data;
using Skinet.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skinet.API.Extensions
{
	public static class ApplicationServicesExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddSingleton<IResponseCacheService, ResponseCacheService>(); 
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped<IBasketRepository, BasketRepository>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IPaymentService, PaymentService>();

			// this is for validation error codes (like when the parameters are not valid)
			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = actionContext => {
					var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
								.SelectMany(x => x.Value.Errors)
								.Select(x => x.ErrorMessage);
					var errorResponse = new ApiValidationErrorResponse() { Errors = errors };

					return new BadRequestObjectResult(errorResponse);
				};
			});

			return services;
		}
	}
}
