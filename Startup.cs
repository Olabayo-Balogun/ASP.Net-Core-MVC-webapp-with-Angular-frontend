using DutchTreat.Data;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
        //The "Configure" method works with the "ConfigureServices" method as such when the program is running or tries to run it looks to the "ConfigureServices" method for the services it needs.

        //When the middleware "app.UseEndpoints" is called, the services listed below (which are what the server needs) will be referred to in order to respond to the call of that middleware

        //The "services.AddControllers" is used strictly for APIs. When you're going to be returning webpages you need to us the service below.
        //Code
        // services.AddControllersWithViews();

        //The service below is used to enable us to use entity framework to create and manipulate a database
        //The DbContext class we created (DutchContext which inherited from DbContext) is something we need to specify in the angle brackets so as to enable the service to know which class we're using as a DbContext class
        //Recall that DbContext class is where we declared the entities that we will be creating a database table for
        //You're going to need to add the "using DutchTreat.Data;" for the service to know where to find the DbContext class.
        //For the DbContext parameter, we need to specify which database we're using as such we used the lambda to create a declaration specifying that we're using SqlServer.
        //You need the "using Microsoft.EntityFrameworkCore;" namespace to make it work
        //Code
        //services.AddDbContext<DutchContext>(cfg =>
        //    {
        //        cfg.UseSqlServer();
        //    });

        //The code above still needs more configuration to work as such a better method is needed
        //Go to the DutchContext class and create an overriding method that will be used to make the process of updating and creating tables better.
        //The code will be written in more detail there as this class is already filled with a lot of comments
        services.AddDbContext<DutchContext>();

        //We're using this to map to directly to the database
        //We need to strengthen this method through some actions.
        //This service makes the "AddTransient" service scoped
        //services.AddDbContext<DutchContext>(cfg =>
        //    {
        //        cfg.UseSqlServer(_config.GetConnectionString("DutchConnectionStrings"));
        //    });

            //Check the usecase for Transient service below
            //The dependency injection is what will ensure the creation of the code below
            //This is a scoped service (though it's not very apparent here) as such we have to write some extra code in the "RunSeeding" method in the "program" class
            //The "AddDbContext" above makes this service scoped
            services.AddTransient<DutchSeeder>();

            //The Singleton service below is used when you have a service that you want to reuse over and over again
            //services.AddSingleton

            //The Scoped service below is used when you have a type of request which should trigger the same process/response everytime
            //services.AddScoped

            //The Transient service below is used when you want to trigger a process that kills itself after it has run, it can however remain if it's being used by other users. It's the middleground between the Singleton and Scoped service
            //The code below declares "IMailService" as what is being injected and "NullMailService" as the concrete class/type that's sent in
            //The NullMailService class can be changed to something else if a more production-worthy alternative is created
            //It is possible to specify which of the AddTransient service will work in which environment writing the env.IsProduction and/or env.IsDevelopment block of code here as is done below in the ConfigureServices method
            //Note that you have to inject this in the controller that will be using the service by creating a constructor in that controller
            services.AddTransient<IMailService, NullMailService>();
           
            services.AddControllersWithViews()

                 //This code below tells the system to recompile the razor pages (upon request for that page) in a situation where the razor page requested for has changed
                 //The feature is useful in development so as to avoid rebuilding the project before razor pages recompile (which was previously needed for the changes you made to take effect) especially when you're making frequent changes to the page and it's already running.
                 //To use this feature you need to install the "Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation" nuget package. You must install it into the project for this to work.
                 .AddRazorRuntimeCompilation();

            //This service allows the program to recognize razor pages 
            //You also need to map to it in the "Configure" method by simply typing "cfg.MapRazorPages();" under the "app.UseEndpoints" middleware.
            services.AddRazorPages();
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

            //This code below is what helps us log errors when we are out of development
            else
            {
                app.UseExceptionHandler("/error");
            }


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
                //This allows the endpoint to use the name of the razor page name for the view.
                cfg.MapRazorPages();

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
