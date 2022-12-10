using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Entities.OrderAggregate
{
	public class OrderItem : BaseEntity
	{
		public OrderItem()
		{

		}
		public OrderItem(ProductItemOrdered itemOrdered, decimal price, int quantity)
		{
			ItemOrdered = itemOrdered;
			Quantity = quantity;
			Price = price;
		}
		public ProductItemOrdered ItemOrdered { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
	}
}
