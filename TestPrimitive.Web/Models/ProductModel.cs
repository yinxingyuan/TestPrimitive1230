using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestPrimitive.Entities;

namespace TestPrimitive.Web.Models
{
	public class ProductModel: CommonModel<Product>
	{
		public bool Sex {get; set;}
		public DateTime CreateTime {get; set;}
		public double Price {get; set;}

		public override void PopulateFrom(Product entity)
		{
			if (entity == null) return;
			base.PopulateFrom(entity);

			this.Sex = entity.Sex;
			this.CreateTime = entity.CreateTime;
			this.Price = entity.Price;
		}

		public override void PopulateTo(Product entity)
		{
			if (entity == null) return;
			base.PopulateTo(entity);

			entity.Sex = this.Sex;

			entity.CreateTime = this.CreateTime;

			entity.Price = this.Price;

		}
	}
}
