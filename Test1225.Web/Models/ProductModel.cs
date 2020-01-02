using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Test1225.Entities;

namespace Test1225.Web.Models
{
	public class ProductModel: CommonModel<Product>
	{

		public override void PopulateFrom(Product entity)
		{
			if (entity == null) return;
			base.PopulateFrom(entity);

		}

		public override void PopulateTo(Product entity)
		{
			if (entity == null) return;
			base.PopulateTo(entity);

		}
	}
}
