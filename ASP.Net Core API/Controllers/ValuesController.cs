using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Service.ProductService;
using Entity.Table;

namespace ASP.Net_Core_API.Controllers
{
	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		private IProductService _productService;

		public ValuesController(IProductService productService)
		{
			_productService = productService;
		}
		// GET api/values
		[HttpGet]
		private IEnumerable<string> Get()
		{
			var result = _productService.Test();
			return new string[] { "value1", result };
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public Product Get(long id)
		{
			return _productService.GetById(id);
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody]string value)
		{
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}

		// GET api/values/5
		[HttpGet]
		[Route("GetList/{category}")]
		public IEnumerable<Product> GetList(int category)
		{
			return _productService.GetByQuery(category.ToString());
		}
	}
}
