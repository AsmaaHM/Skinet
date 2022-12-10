using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skinet.API.Dtos
{
	public class OrderDto
	{
		public string BasketId { get; set; }

		public int DeliveryMethodId { get; set; }

		public AddressDto ShipToAddress { get; set; }


	}
}
