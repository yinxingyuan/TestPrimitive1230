using Test1225.Entities;
using System.Collections.Generic;
using Test1225.Services.Interfaces;
using Test1225.Web.Models;
using MetaShare.Common.Core.CommonService;
using MetaShare.Common.Core.Entities;


namespace Test1225.Web.Controllers
{
	public class ProductController:CommonController<Product, IProductService, ProductModel>
	{


		protected override List<Product> GetBySearchModel(SearchModel pagerSearchModel)
        {
	        if (pagerSearchModel == null) return this.GetPagerData(new Pager { PageIndex = 1, PageSize = PageSize });

            List<Product> lists = this.Service.SelectBy(pagerSearchModel.Pager,new Product { Name = pagerSearchModel.Name }, product => product.Name.Contains(pagerSearchModel.Name));
        return lists;
	}

}
}
