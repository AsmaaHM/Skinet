using Core.Interfaces;
using Skinet.Core;
using Skinet.Core.Entities.OrderAggregate;
using Skinet.Core.Interfaces;
using Skinet.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Infrastructure.Services
{
	public class OrderService : IOrderService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBasketRepository _basketRepo;
		private readonly IPaymentService _paymentService;

		public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepo, IPaymentService paymentService)
		{
			_unitOfWork = unitOfWork;
			_basketRepo = basketRepo;
			_paymentService = paymentService;
		}
		public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
		{
			var basket = await _basketRepo.GetBasketAsync(basketId);
			var items = new List<OrderItem>();
			foreach (var item in basket.Items)
			{
				var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
				var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
				var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
				items.Add(orderItem);
			}
			var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
			var subtotal = items.Sum(item => item.Price*item.Quantity);

			var spec = new OrderByPaymentIntentIdSpecification(basket.PaymentIntentId);
			var exisitingOrder = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
			if (exisitingOrder != null)
			{
				_unitOfWork.Repository<Order>().Delete(exisitingOrder);
				await _paymentService.CreateOrUpdatePaymentIntent(basket.PaymentIntentId);
			}


			var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal, basket.PaymentIntentId);

			_unitOfWork.Repository<Order>().Add(order);
			var result = await _unitOfWork.Complete();

			if (result <= 0) return null;

			return order; 
		}

		public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
		{
			return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync(); 
		}

		public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
		{
			var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);
			return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
		}

		public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
		{
			var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);
			return await _unitOfWork.Repository<Order>().ListAsync(spec); 
		}
	}
}
