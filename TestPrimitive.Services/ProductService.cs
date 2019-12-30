using System.Collections.Generic;
using MetaShare.Common.Core.Entities;
using TestPrimitive.Entities;
using MetaShare.Common.Core.Services;
using TestPrimitive.Daos.Interfaces;
using TestPrimitive.Services.Interfaces;


namespace TestPrimitive.Services
{
	public class ProductService : Service<Product>, IProductService
	{
		public ProductService() : base(typeof (IProductDao))
		{
		}

	}
}
