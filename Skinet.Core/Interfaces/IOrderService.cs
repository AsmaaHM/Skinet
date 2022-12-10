using Skinet.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Interfaces
{
	public interface IOrderService
	{
		Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shiipingAddress);

		Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);

		Task<Order> GetOrderByIdAsync(int id, string byuderEmail);

		Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync(); 
	}
}
