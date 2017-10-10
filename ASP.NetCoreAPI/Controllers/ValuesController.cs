using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Service.ProductService;
using Entity.Table;
using Common;
using Microsoft.AspNetCore.Cors;

namespace ASP.NetCoreAPI.Controllers
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
		[EnableCors(ConstValues.CorsValue)] //设置跨域处理的 代理
		private IEnumerable<string> Get()
		{
			var result = _productService.Test();
			return new string[] { "value1", result };
		}

		// GET api/values/5
		[HttpGet("{id}")]
		[EnableCors(ConstValues.CorsValue)] //设置跨域处理的 代理
		public Product Get(long id)
		{
			return _productService.GetById(id);
		}

		// POST api/values
		[HttpPost]
		[EnableCors(ConstValues.CorsValue)] //设置跨域处理的 代理
		public void Post([FromBody]string value)
		{
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		//[HttpDelete("{id}")]
		[HttpGet("Delete/{id}")]
		public bool Delete(long id)
		{
			try
			{
				_productService.DeleteById(id);
				return true;
			}
			catch (System.Exception ex)
			{
				return false;
			}

		}

		// DELETE api/values/
		//[HttpDelete]
		[HttpGet("DeleteAll")]
		public bool DeleteAll()
		{
			try
			{
				_productService.DeleteAll();
				return true;
			}
			catch (System.Exception)
			{
				return false;
			}

		}

		// GET api/values/5
		[HttpGet]
		[Route("GetList/{category}")]
		[EnableCors(ConstValues.CorsValue)] //设置跨域处理的 代理
		public IEnumerable<Product> GetList(int category)
		{
			return _productService.GetByQuery(category.ToString());
		}
	}
}
