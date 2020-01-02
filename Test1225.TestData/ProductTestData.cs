using System;
using System.Collections.Generic;
using NUnit.Framework;
using Test1225.Entities;

namespace Test1225.TestData
{
	public class ProductTestData
	{
		public const int ProductCount = 3;

		 public static List<Product> CreateProduct()
		{
			return new List<Product>
			{
				CreateProduct1(),
				CreateProduct2(),
				CreateProduct3(),
			};
		}

		 public static Product CreateProduct1()
		{
			return new Product
			{
					Id = 1, 
					Name = string.Empty, 
					Description = string.Empty, 
			};
		}
		 public static Product CreateProduct2()
		{
			return new Product
			{
					Id = 2, 
					Name = string.Empty, 
					Description = string.Empty, 
			};
		}
		 public static Product CreateProduct3()
		{
			return new Product
			{
					Id = 3, 
					Name = string.Empty, 
					Description = string.Empty, 
			};
		}
		public static void AssertAreEqual(Product expected, Product actual)
		{
			Assert.AreEqual(expected.Name, actual.Name);
			Assert.AreEqual(expected.Description, actual.Description);
		}
	}
}
