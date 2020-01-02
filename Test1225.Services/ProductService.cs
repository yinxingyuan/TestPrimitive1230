using System.Collections.Generic;
using MetaShare.Common.Core.Entities;
using Test1225.Entities;
using MetaShare.Common.Core.Services;
using Test1225.Daos.Interfaces;
using Test1225.Services.Interfaces;


namespace Test1225.Services
{
	public class ProductService : Service<Product>, IProductService
	{
		public ProductService() : base(typeof (IProductDao))
		{
		}

	}
}
