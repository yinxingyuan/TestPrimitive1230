using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestPrimitive.Entities;
using TestPrimitive.Services.Interfaces;
using TestPrimitive.WebApi.Models;

namespace TestPrimitive.WebApi.Controllers
{
	[Route("/ProductApi")]
	public class ProductApiController : CommonApiController<Product, IProductService>
	{


	}
}
