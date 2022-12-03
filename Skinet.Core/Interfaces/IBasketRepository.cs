using Skinet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Interfaces
{
	public interface IBasketRepository
	{
		Task<CustomerBasket> GetBasketAsync(string BasketId);

		Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);

		Task<bool> DeleteBAsketAsync(string BasketId);


	}
}
