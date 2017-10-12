using DAL.Mapping;
using Entity.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
	public class ProductContext : DbContext
	{
		//https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/complex-data-model
		public ProductContext(DbContextOptions<ProductContext> options) : base(options)
		{
			//在此可对数据库连接字符串做加解密操作
		}

		public DbSet<Product> Product { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			#region
			//foreach (var entityType in modelBuilder
			//	.Model.GetEntityTypes()
			//	.SelectMany(t => t.GetProperties())
			//	.Where(p => p.ClrType == typeof(decimal)))
			//{
			//}
			#endregion
			base.OnModelCreating(modelBuilder);
			modelBuilder.AddEntityConfigurationsFromAssembly(GetType().Assembly);
		}
	}
}
