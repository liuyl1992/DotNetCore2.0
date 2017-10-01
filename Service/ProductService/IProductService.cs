using Entity.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.ProductService
{
	public interface IProductService
	{
		string Test();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Product GetById(long id);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		IEnumerable<Product> GetByQuery(params string[] query);

	}
}
