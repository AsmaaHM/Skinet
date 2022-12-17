using Skinet.Core.Entities;
using Skinet.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Interfaces
{
	public interface IPaymentService
	{
		Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);

		Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId);
		Task<Order> UpdateOrderPaymentFailed(string paymentIntentId);
	}
}
