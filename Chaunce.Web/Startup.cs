using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Chaunce.Web.Services;
using Identity.Extensitions.Model;
using System.Reflection;
using DAL;

namespace Chaunce.Web
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
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseMySql(Configuration.GetConnectionString("MySqlConnection")));

			foreach (var item in GetClassName("Service"))
			{
				foreach (var typeArray in item.Value)
				{
					services.AddScoped(typeArray, item.Key);
				}
			}

			services.AddUnitOfWork<ProductContext>();//添加UnitOfWork支持

			services.AddIdentity<ApplicationUser, CustomRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			#region
			services.Configure<IdentityOptions>(options =>
			{
				// Password settings
				options.Password.RequireDigit = false;
				options.Password.RequiredLength = 8;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;
				options.Password.RequiredUniqueChars = 6;

				// Lockout settings
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
				options.Lockout.MaxFailedAccessAttempts = 10;
				options.Lockout.AllowedForNewUsers = true;

				// User settings
				options.User.RequireUniqueEmail = true;
			});

			services.ConfigureApplicationCookie(options =>
			{
				// Cookie settings
				options.Cookie.HttpOnly = true;
				options.Cookie.Expiration = TimeSpan.FromDays(150);
				options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
				options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
				options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
				options.SlidingExpiration = true;
			});
			#endregion

			// Add application services.
			services.AddTransient<IEmailSender, EmailSender>();

			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles(new StaticFileOptions
			{
				OnPrepareResponse = context =>
				{
					//context.Context.Response.Headers.Add("cache-control", new[] { "public,no-cache" });
					//以下操作是UseStaticFiles内部默认实现
					//Cache static file for 1 year
					if (!string.IsNullOrEmpty(context.Context.Request.Query["v"]))//资源添加asp-append-version="true"后v是查询参数
					{
						context.Context.Response.Headers.Add("cache-control", new[] { "public,max-age=31536000" });

						context.Context.Response.Headers.Add("Expires", new[] { DateTime.UtcNow.AddYears(1).ToString("R") }); // Format RFC1123
					}
				}
			});
			
			//app.UseStaticFiles();

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller}/{action}/{id?}",
					defaults: new { controller = "Home", action = "Index" });
			});
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
		
		/// <summary>
		/// 一个接口对应多个实现
		/// </summary>
		/// <param name="assemblyName"></param>
		/// <returns></returns>
		public Dictionary<Type, Type[]> GetName(string assemblyName)
		{
			if (!String.IsNullOrEmpty(assemblyName))
			{
				Assembly assembly = Assembly.Load(assemblyName);
				List<Type> ts = assembly.GetTypes().ToList();

				var result = new Dictionary<Type, Type[]>();
				//找出所有接口
				foreach (var item in ts.Where(s => s.IsInterface))
				{
					var instances = new List<Type>();
					 var instance= ts.Where(s => !s.IsInterface);
					foreach (var inter in instance)
					{
						if (item.IsAssignableFrom(inter))
						{
							instances.Add(inter);
						}
					}
					result.Add(item, instances.ToArray());

				}
				return result;
			}
			return new Dictionary<Type, Type[]>();
		}
	}
}
