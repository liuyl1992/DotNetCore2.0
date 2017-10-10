using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DAL;
using Service.ProductService;
using Common;

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

			services.AddUnitOfWork<ProductContext>();//添加UnitOfWork支持
			services.AddScoped(typeof(IProductService), typeof(ProductService));//用ASP.NET Core自带依赖注入(DI)注入使用的类
			services.AddMvc();

			//配置跨域处理，参考：http://www.cnblogs.com/tianma3798/p/6920704.html;https://docs.microsoft.com/en-us/aspnet/core/security/cors
			services.AddCors(options => options.AddPolicy(ConstValues.CorsValue,
			p => p.WithOrigins("https://127.0.0.1:5443", "http://127.0.0.1").AllowAnyMethod().AllowAnyHeader()));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc();
			app.UseCors(ConstValues.CorsValue);
		}
	}
}