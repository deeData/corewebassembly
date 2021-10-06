using DataStore.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApp
{
    public class Startup
    {
        //setup env options in order to use in-memory database in development
        private readonly IWebHostEnvironment _env;
        public Startup(IWebHostEnvironment env) 
        {
            this._env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //use in-memory db in development- created at every build
            if (_env.IsDevelopment())
            {
                //default as Scoped lifetime
                services.AddDbContext<BugsContext>(options =>
                {
                    options.UseInMemoryDatabase("Bugs");
                });
            }
            //apply filter to all controllers
            //services.AddControllers(options => {
            //    options.Filters.Add<Version1DiscontinueResourceFilter>();
            //});
            services.AddControllers();

            services.AddApiVersioning(options => {
                //to include in the response header- what version came back
                options.ReportApiVersions = true;
                //version will be in header not url
                options.AssumeDefaultVersionWhenUnspecified = true;
                //if no version specified in header, then use version 1,0 (version # is specified at Controller) 1,0 means 1.0
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1,0);
                //can specify any header but usually starts with X-
                options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
            });
        }





        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BugsContext bugsContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //create new in-memory db each time
                bugsContext.Database.EnsureDeleted();
                bugsContext.Database.EnsureCreated();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //http context - context.Request
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapControllers();
            });
        }
    }
}
