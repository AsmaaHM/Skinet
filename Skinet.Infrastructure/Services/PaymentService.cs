using Microsoft.Extensions.Configuration;
using Skinet.Core.Entities;
using Skinet.Core.Entities.OrderAggregate;
using Skinet.Core.Interfaces;
using Skinet.Core.Specifications;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Skinet.Core.Product;

namespace Skinet.Infrastructure.Services
{
	public class PaymentService : IPaymentService
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IConfiguration _configuration;


		public PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IConfiguration configuration)
		{
			_basketRepository = basketRepository;
			_unitOfWork = unitOfWork;
			_configuration = configuration;
		}
		public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
		{
			StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
			var basket = await _basketRepository.GetBasketAsync(basketId);

			if(basket == null)
				return null; 

			var shippingPrice = 0m;

			if (basket.DeliveryMethodId.HasValue)
			{
				var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>()
					.GetByIdAsync((int)basket.DeliveryMethodId);
				shippingPrice = deliveryMethod.Price; 
			}

			foreach (var item in basket.Items)
			{
				var productItem = await _unitOfWork.Repository<Product>()
					.GetByIdAsync(item.Id);
				if (item.Price != productItem.Price)
					item.Price = productItem.Price;
			}

			var service = new PaymentIntentService();

			PaymentIntent intent;

			if (String.IsNullOrEmpty(basket.PaymentIntentId))
			{
				var options = new PaymentIntentCreateOptions()
				{
					Amount = (long)basket.Items.Sum(item => (item.Price * 100) * item.Quantity) + (long)shippingPrice * 100,
					Currency = "usd",
					PaymentMethodTypes = new List<string> { "card" }
				};

				intent = await service.CreateAsync(options);
				basket.PaymentIntentId = intent.Id;
				basket.ClientSecret = intent.ClientSecret;
			}
			else 
			{
				var options = new PaymentIntentUpdateOptions()
				{
					Amount = (long)basket.Items.Sum(item => (item.Price * 100) * item.Quantity) + (long)shippingPrice * 100
				};

				intent = await service.UpdateAsync(basket.PaymentIntentId, options);
			}

			await _basketRepository.UpdateBasketAsync(basket);
			return basket;
		}

		public async Task<Order> UpdateOrderPaymentFailed(string paymentIntentId)
		{
			var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
			var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
			order.Status = OrderStatus.PaymentFailed;
			await _unitOfWork.Complete();

			return order; 
		}

		public async Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId)
		{
			var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
			var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

			if(order == null) return null;

			order.Status = OrderStatus.PaymentReceived;
			_unitOfWork.Repository<Order>().Update(order);

			await _unitOfWork.Complete();

			return order;
		}
	}
}
