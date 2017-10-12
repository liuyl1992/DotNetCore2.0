using Entity.Table;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Mapping
{
	public class ProductConfiguration : EntityMappingConfiguration<Product>
	{
		public override void Map(EntityTypeBuilder<Product> b)
		{
			//b.ToTable("Product", "HumanResources")
			//	.HasKey(p => p.Id);

			//b.Property(p => p.Name).HasMaxLength(50).IsRequired();
		}
	}
}
