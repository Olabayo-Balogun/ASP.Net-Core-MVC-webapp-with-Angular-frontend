using DutchTreat.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            //var host = CreateHostBuilder(args).Build();

            //RunSeeding(host);
            //In order to run the seeding automatically, we have to write some code that enables us to do that here.
            var host = CreateHostBuilder(args).Build();
            //The conditional statement below confirms that there is in fact seed data and it is a seed argument
            //If there's seeding it'll run it, if there isn't the seeding won't be run, just the host.
            if (args.Length > 0 && args[0].ToLower() == "/seed")
            {
                RunSeeding(host);
                return;
            }
            else
            {
                host.Run();
            }      
        }

        //Before we can run the seeding, we need to get the seed class
        //private static void RunSeeding(IWebHost host)
        //{
            //We are using this variable to get the seed class
            //You need the "using Microsoft.Extensions.DependencyInjection;" namespace to get the generic version of the "DutchSeeder" class
            //var seeder = host.Services.GetService<DutchSeeder>();

            //seeder.Seed();
            //To make all the above work successfully, we need to insert more code into the "Startup.cs" class
        //}


        //We're using this method instead of the one above because of the scoping of the "DutchSeeder" in the "Startup" class
        private static void RunSeeding(IHost host)
        {
            //For every request, the "scopeFactory" creates a scope for the entire lifetime of the request
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();

            //The "using" is used so that the scope closes after the work is done and we no longer need it.
            using (var scope = scopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<DutchSeeder>();
                seeder.Seed();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //The ".ConfigureAppConfiguration()" declaration simply tells the project that you want to configure application configuration (which usually happens in the "Startup.cs" class)
                //Also not that I deleted the "appsettings.json" file in a bid to build it from the ground up and understand everything that is going on in that file
                .ConfigureAppConfiguration(SetupConfiguration)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        //Note that "ctx" means "context" (which is a parameter name we gave to "HostBuilderContext" class)
        //Note that "bldr" means "builder" (which is a parameter name we gave to "IConfigurationBuilder" class)
        //Also note that it isn't advisable to name your parameters this way, always favor clear words over abbreviations
        private static void SetupConfiguration(HostBuilderContext ctx, IConfigurationBuilder builder)
        {
            //The line of code below gets rid of the need for the AppConfiguration file
            //Note that you don't need to do these stuff in your normal projects, the knowledge however can be userful in future
            builder.Sources.Clear();

            //The line of code below declares the location of a configuration file we intend to use in this project seeing as we deleted the scaffolded one in favor of learning how to build things from the ground up.
            // builder.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("config.json")

            builder.AddJsonFile("config.json", false, true)
               //Adding the ".AddEnvironmentVariables()" declaration is especially useful when you're dealing with cloud deployment to define configuration pieces
               //This basically builds a configuration based on the sources you're putting into it,
               //The order matters when chaining these methods, this "config.json" file can be a default that is overrideable by the ".AddEnvironmentVariables()" method especially when the method has it's own name or path
               .AddEnvironmentVariables();
        }
    }
}
