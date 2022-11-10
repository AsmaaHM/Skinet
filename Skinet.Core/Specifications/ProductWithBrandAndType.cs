using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Specifications
{
	public class ProductWithBrandAndType : BaseSpecification<Product> 
	{
		public ProductWithBrandAndType()
		{
			AddInclude(x => x.Brand);
			AddInclude(x => x.Type);
		}

		public ProductWithBrandAndType(int id) : base(x=> x.Id == id)
		{
			AddInclude(x => x.Brand);
			AddInclude(x => x.Type);
		}
	}
}
