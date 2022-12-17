using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Skinet.API.Dtos
{
	public class CustomerBasketDto
	{
		[Required]
		public string Id { get; set; }

		public List<BasketItemDto> Items { get; set; }

		public int? DeliveryMethodId { get; set; }

		public string ClientSecret { get; set; }

		public string PaymentIntentId { get; set; }
		public decimal ShippingAddress { get; set; }
	}

	public class BasketItemDto
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string ProductName { get; set; }
		[Required]
		[Range(0.1, double.MaxValue, ErrorMessage ="Price must be greater than 0")]
		public decimal Price { get; set; }
		[Required]
		[Range(1, double.MaxValue, ErrorMessage ="Quantity must be at least 1")]
		public int Quantity { get; set; }
		[Required]
		public string PictureUrl { get; set; }
		[Required]
		public string Brand { get; set; }
		[Required]
		public string Type { get; set; }
	}
}
