using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Entity.Table;
using DAL;
using System.Reflection;
using Service;
using Common.Const;

namespace ASP.NetCoreAPI
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ProductContext>(options =>
				options.UseMySql(Configuration.GetConnectionString("MySqlConnection")));//添加Mysql支持
																						//集中注册服务
			foreach (var item in GetClassName("Service"))
			{
				foreach (var typeArray in item.Value)
				{
					services.AddScoped(typeArray, item.Key);
				}
			}
			services.AddUnitOfWork<ProductContext>();//添加UnitOfWork支持
			services.AddMvc();

			//配置跨域处理，参考：
			//https://docs.microsoft.com/en-us/aspnet/core/security/cors
			//https://blog.johnwu.cc/article/asp-net-core-cors.html?from=singlemessage&isappinstalled=0
			//http://www.cnblogs.com/tianma3798/p/6920704.html
			//指定来源
			//services.AddCors(options => options.AddPolicy(ConstValues.CorsValue,
			//p => p.WithOrigins("http://localhost:48057", "http://127.0.0.1").AllowAnyMethod().AllowAnyHeader()));

			//允许所有来源
			services.AddCors(options =>
			options.AddPolicy(ConstValues.CorsValue,
			p => p.AllowAnyOrigin())
			);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			// 套用 Policy 到 Middleware()
			app.UseCors(ConstValues.CorsValue);//此代码是作用于全局,每个controller和action自动允许跨域
			app.UseMvc();
		}

		/// <summary>  
		/// 获取程序集中的实现类对应的多个接口
		/// </summary>  
		/// <param name="assemblyName">程序集</param>
		public Dictionary<Type, Type[]> GetClassName(string assemblyName)
		{
			if (!String.IsNullOrEmpty(assemblyName))
			{
				Assembly assembly = Assembly.Load(assemblyName);
				List<Type> ts = assembly.GetTypes().ToList();

				var result = new Dictionary<Type, Type[]>();
				foreach (var item in ts.Where(s => !s.IsInterface))
				{
					var interfaceType = item.GetInterfaces();
					result.Add(item, interfaceType);
				}
				return result;
			}
			return new Dictionary<Type, Type[]>();
		}
	}
}