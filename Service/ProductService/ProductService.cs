using Entity.Table;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Service.ProductService
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;
		public ProductService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public string Test()
		{
			var repo = _unitOfWork.GetRepository<Product>();
			repo.Insert(new Product
			{
				Category = 1,
				Description = "此商品为澳洲代购,买不了吃亏买不了上当",
				Discount = (decimal)899.21,
				Price = (decimal)98.2,
				Name = "澳洲袋鼠粉",
			});
			_unitOfWork.SaveChanges();//提交到数据库
			var result = repo.Find((long)2).Description;
			//var result = repo.GetFirstOrDefault()?.Description ?? string.Empty;
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Product GetById(long id)
		{
			var repo = _unitOfWork.GetRepository<Product>();
			return repo.Find(id);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public IEnumerable<Product> GetByQuery(params string[] query)
		{
			var repo = _unitOfWork.GetRepository<Product>();
			return repo.FromSql("select * from Product where Category={0}", query);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public void DeleteById(long id)
		{
			var repo = _unitOfWork.GetRepository<Product>();
			repo.Delete(id);
			_unitOfWork.SaveChanges();//提交到数据库
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public void DeleteAll()
		{
			var repo = _unitOfWork.GetRepository<Product>();
			repo.FromSql("delete Product");
			_unitOfWork.SaveChanges();//提交到数据库
		}
	}
}
