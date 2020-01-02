using System;
using System.Web.Http;
using System.Collections.Generic;
using Test1225.Entities;
using Test1225.Services.Interfaces;
using Test1225.WebApi.Models;

namespace Test1225.WebApi.Controllers
{
	public class ProductApiController : CommonApiController<Product, IProductService>
	{


	}
}
