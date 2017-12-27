using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore
{
  public class Startup
  {
    public Startup(IConfiguration configuration) =>
      Configuration = configuration;

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services
        .AddDbContext<ApplicationDbContext>(options =>
                                            options.UseNpgsql(
                                              Configuration["Data:SportsStoreProducts:ConnectionString"]));
      services.AddTransient<IProductRepository, EFProductRepository>();
      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

			app.UseStatusCodePages();
      app.UseStaticFiles();
      app.UseMvc(routes => {
        routes.MapRoute(
          name: "pagination",
          template: "products/page{productPage}",
          defaults: new { Controller = "Product", action = "List" });
        
        routes.MapRoute(
          name: "default",
          template: "{controller=Product}/{action=List}/{id?}"
        );
      });
      SeedData.EnsurePopulated(app);
    }
  }
}
