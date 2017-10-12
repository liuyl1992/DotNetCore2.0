using Entity.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
	public class TestService : ITestinternalService
	{
		private readonly IUnitOfWork _unitOfWork;
		public TestService(IUnitOfWork unitOfWork)
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

			var result = repo.GetFirstOrDefault()?.Description ?? string.Empty;
			return result;
		}

	}
}
