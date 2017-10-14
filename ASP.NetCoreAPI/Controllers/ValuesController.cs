using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Service.ProductService;
using Entity.Table;
using Microsoft.AspNetCore.Cors;
using Service;
using Common.Core;
using Common;

namespace ASP.NetCoreAPI.Controllers
{
	public class ValuesController : BaseApiController
	{
		private IProductService _productService;
		private ITestService _testService;

		public ValuesController(IProductService productService
			, ITestService testService)
		{
			_productService = productService;
			_testService = testService;
		}
		// GET api/values
		[HttpGet]
		public IActionResult Get()
		{
			var result = _productService.Test();
			var result1 = _testService.Test();
			return SendResult(ErrorCode.OK, new { result, result1 });
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public IActionResult Get(long id)
		{
			return SendResult(ErrorCode.OK, _productService.GetById(id));
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
		public IActionResult GetList(int category)
		{
			var result = _productService.GetByQuery(category.ToString());
			return SendResult(ErrorCode.OK, result);
		}
	}
}
