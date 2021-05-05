using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //The code tells the program to use and run staic files like html pages
            //Note that the code below only works on static files found in the www.root folder
            app.UseStaticFiles();

            //The code below wrapped in an if block simply specifies what kind of exception page to render
            //In this case, the environment is a development environment
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //The code below enables routing
            app.UseRouting();

            //The code below allows the use of endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });


            //Code
            //The commented code below will still run fine only it won't have a defined exception page
            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});

            //Code
            //The code below is the least possible startup code that can run
            //what it does is simply return the code found in the parameter regardless of the route you use
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
