using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Specifications
{
	public class ProductWithBrandAndType : BaseSpecification<Product> 
	{
		public ProductWithBrandAndType(ProductSpecParams productParams)
			: base(x=> 
			    (String.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search))
			    && (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId)
				&& (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
			)
		{
			AddInclude(x => x.Brand);
			AddInclude(x => x.Type);
			ApplyPaging(productParams.PageSize* (productParams.PageIndex - 1), productParams.PageSize);

			if (!string.IsNullOrEmpty(productParams.Sort))
			{
				switch (productParams.Sort)
				{
					case "priceAsc":
						AddOrderBy(x => x.Price);
						break;
					case "priceDesc":
						AddOrderByDesc(x => x.Price);
						break;
					default:
						AddOrderBy(x => x.Name);
						break;
				};
			}
		}

		public ProductWithBrandAndType(int id) : base(x=> x.Id == id)
		{
			AddInclude(x => x.Brand);
			AddInclude(x => x.Type);
		}
	}
}
