﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Skinet.API.Dtos;
using Skinet.API.Errors;
using Skinet.Core.Entities;
using Skinet.Core.Entities.OrderAggregate;
using Skinet.Core.Interfaces;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Skinet.API.Controllers
{

	public class PaymentsController :BaseApiController
	{
		private readonly IPaymentService _paymentService;
		private readonly ILogger<PaymentsController> _logger;
		private const string WhSecret = "whsec_147fb3f9ab841002957582559e25cb37a2d76513c05f9619c9d6371412c128ae"; 

		public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger)
		{
			_paymentService = paymentService;
			_logger = logger;
		}


		[Authorize]
		[HttpPost("{basketId}")]
		public async Task<ActionResult<CustomerBasket>> CreareOrUpdatePaymentIntent(string basketId) {

			var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
			if (basket == null) return BadRequest(new ApiResponse(400, "Problem with your basket"));

			return basket;
		} 

		[HttpPost("Webhook")]
		public async Task<ActionResult> StripeWebhook()
		{

			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
			var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], WhSecret);
			PaymentIntent intent;
			Order order;

			switch (stripeEvent.Type)
			{

				case "payment_intent.succeeded":
					intent = (PaymentIntent)stripeEvent.Data.Object;
					_logger.LogInformation("Payment succeeded " + intent);
					order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
					_logger.LogInformation("Order updated to payment received: " + order.Id);
					break;
				case "payment_intent.payment_failed":
					intent = (PaymentIntent)stripeEvent.Data.Object;
					_logger.LogInformation("Payment Failed: ", intent.Id);
					order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
					_logger.LogInformation("Order updated to payment failed: " + order.Id);
					break; 
			}

			return new EmptyResult(); 
		}

	}
}
