using MetaShare.Common.Core.Daos;
using Test1225.Entities;
using Test1225.Daos.Interfaces;
using Test1225.TestData;

namespace Test1225.Daos.Mocks
{
	public class ProductDaoMock : MockDao<Product>, IProductDao
	{
		public ProductDaoMock() : base(ProductTestData.CreateProduct())
		{
		}
	}
}
