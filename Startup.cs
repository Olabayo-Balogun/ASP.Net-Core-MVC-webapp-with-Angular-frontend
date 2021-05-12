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
            //The "Configure" method works with the "ConfigureServices" method as such when the program is running or tries to run it looks to the "ConfigureServices" method for the services it needs.
            //When the middleware "app.UseEndpoints" is called, the services listed below (which are what the server needs) will be referred to in order to respond to the call of that middleware
            //The "services.AddControllers" is used strictly for APIs. When you're going to be returning webpages you need to us the service below.
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Note that the arrangement order of your middleware matters here.
            //Middlewares are basically logical instructions that the program has to execute

            //The middleware below handles exception pages and shows them to developers only which means the people who are working on the program because the environment shown to developers working on the project is the development environment
            //The program knows it's in development by looking through the properties of the project in the "Debug" section which is where the environment is specified.
            //Without the middleware below, the program won't know how to respond to unhandled exceptions and it'll fail badly.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //The code above can also be written as 
            //Code
            //if (env.IsEnvironment("Development"))
            //{
            //    app.UseDeveloperExceptionPage();
            //}



            //The code below tells the project to use default files that it finds when it looks through the www.root folder or other folders in a project.
            //It can be especially useful when you want to automatically render static pages and resources that are well known eg. index.html
            //What it does is changes the path of the project such that it is easier to access especially when used with the "app.UseStaticFiles();" middleware.
            //If you reverse the arrangement order of both this middleware and the UseDefaultFiles middleware the static pages won't be rendered by default except if you specify the url
            //Code
            //app.UseDefaultFiles();

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
            /////Routing enables us to direct calls or queries that go into the server (from the user) to specific pieces of code
            app.UseRouting();


            ////The code below allows the use of endpoints
            //Endpoints allow you to specify a set of middleware that executed in a specific order to satisfy requests/calls (from the user) to a server. 
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

            app.UseEndpoints(cfg =>
            {
                //The code below will allow us to build a format with which url/endpoints will be written and the controller that will handle it. 
                //The question mark after "id" means the id is optional. It follows the same principle of making things nullable.
                cfg.MapControllerRoute("Default", "/{controller}/{action}/{id?}",
                    //The code below is an annonymous object which specifies a defaul route if no specific routing is inputed
                    //What this means that the program will head to the place specified below if the customer doesn't specify any route
                    //The word "App" comes from the "AppController" class, recall that the computer ignores the "Controller" prefix in controller class names as such the name of the controller from the perspective of the computer is "App"
                    //The "Index" is the first method in the "App" class, most likely an index page is going to be the default or homepage of any controller. You can change the "action" parameter to another method if you like though.
                    new { controller = "App", action = "Index" });
            });
        }
    }
}
