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
            //Note that the arrangement order of your middleware matters here.
            //Middlewares are basically logical instructions that the program has to execute

            //The code below tells the project to use default files that it finds when it looks through the www.root folder or other folders in a project.
            //It can be especially useful when you want to automatically render static pages and resources that are well known eg. index.html
            //What it does is changes the path of the project such that it is easier to access especially when used with the "app.UseStaticFiles();" middleware.
            //If you reverse the arrangement order of both this middleware and the UseDefaultFiles middleware the static pages won't be rendered by default except if you specify the url
            app.UseDefaultFiles();

            //The code below tells the program to use and run staic files like html pages
            //Note that the code below only works on static files found in the www.root folder
            app.UseStaticFiles();

            //The code below wrapped in an if block simply specifies what kind of exception page to render
            //In this case, the environment is a development environment
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            ////The code below enables routing
            //app.UseRouting();

            ////The code below allows the use of endpoints
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});


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
