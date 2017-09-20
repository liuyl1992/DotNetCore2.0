using Entity.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Table
{
	/// <summary>
	/// 商品类
	/// </summary>
	public class Product : BaseEntity<long>
	{
		/// <summary>
		/// 名称
		/// </summary>
		[StringLength(20)]
		[Required]
		public string Name { get; set; }

		/// <summary>
		/// 描述
		/// </summary>
		[StringLength(500)]
		[Required]
		public string Description { get; set; }

		/// <summary>
		/// 类别
		/// </summary>
		[Range(1, int.MaxValue)]
		public int Category { get; set; }

		/// <summary>
		/// 原价
		/// </summary>
		[Required]
		public decimal Price { get; set; }

		/// <summary>
		/// 现价
		/// </summary>
		[Required]
		public decimal Discount { get; set; }
	}
}
