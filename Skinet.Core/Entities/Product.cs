using System;
using System.ComponentModel.DataAnnotations;

namespace Skinet.Core
{
	public class Product : BaseEntity
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
		public string PictureUrl { get; set; }
		public int ProductBrandId { get; set; }
		public ProductBrand Brand { get; set; }
		public int ProductTypeId { get; set; }
		public ProductType Type { get; set; }
	}
}
