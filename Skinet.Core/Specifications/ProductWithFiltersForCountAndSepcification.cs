using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Specifications
{
	public class ProductWithFiltersForCountAndSepcification : BaseSpecification<Product> 
	{
		public ProductWithFiltersForCountAndSepcification(ProductSpecParams productParams)
			: base(x=>
				(String.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search))
				&& (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId)
				&& (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
			)
		{
		
		}

	}
}
