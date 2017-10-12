using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using DAL;
using Service.ProductService;

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
            //配置跨域处理，参考：http://www.cnblogs.com/tianma3798/p/6920704.html
           services.AddCors(options =>
        {
            // Policy 名稱 CorsPolicy 是自訂的，可以自己改
            options.AddPolicy("CorsPolicy", policy =>
            {
                // 設定允許跨域的來源，有多個的話可以用 `,` 隔開
                policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
            });
        });
        services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
       public void Configure(IApplicationBuilder app)
    {
        // 套用 Policy 到 Middleware
        app.UseCors("CorsPolicy");
        app.UseMvc();
    }
    }
}