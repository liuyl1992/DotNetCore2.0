using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;
using System.Linq;

namespace DAL.Mapping
{
	//https://stackoverflow.com/questions/26957519/ef-core-mapping-entitytypeconfiguration#comment46244976_26957519
	public interface IEntityMappingConfiguration
	{
		void Map(ModelBuilder b);
	}

	public interface IEntityMappingConfiguration<T> : IEntityMappingConfiguration where T : class
	{
		void Map(EntityTypeBuilder<T> builder);
	}

	public abstract class EntityMappingConfiguration<T> : IEntityMappingConfiguration<T> where T : class
	{
		public abstract void Map(EntityTypeBuilder<T> b);

		public void Map(ModelBuilder b)
		{
			Map(b.Entity<T>());
		}
	}

	public static class ModelBuilderExtenions
	{
		private static IEnumerable<Type> GetMappingTypes(this Assembly assembly, Type mappingInterface)
		{
			return assembly.GetTypes().Where(x => !x.IsAbstract && x.GetInterfaces().Any(y => y.GetTypeInfo().IsGenericType && y.GetGenericTypeDefinition() == mappingInterface));
		}

		public static void AddEntityConfigurationsFromAssembly(this ModelBuilder modelBuilder, Assembly assembly)
		{
			var mappingTypes = assembly.GetMappingTypes(typeof(IEntityMappingConfiguration<>));
			foreach (var config in mappingTypes.Select(Activator.CreateInstance).Cast<IEntityMappingConfiguration>())
			{
				config.Map(modelBuilder);
			}
		}
	}
}
