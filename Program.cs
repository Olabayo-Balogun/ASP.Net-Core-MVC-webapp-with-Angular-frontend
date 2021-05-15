using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //The ".ConfigureAppConfiguration()" declaration simply tells the project that you want to configure application configuration (which usually happens in the "Startup.cs" class)
                //Also not that I deleted the "appsettings.json" file in a bid to build it from the ground up and understand everything that is going on in that file
                .ConfigureAppConfiguration(AddConfiguration)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        //Note that "ctx" means "context" (which is a parameter name we gave to "HostBuilderContext" class)
        //Note that "bldr" means "builder" (which is a parameter name we gave to "IConfigurationBuilder" class)
        //Also note that it isn't advisable to name your parameters this way, always favor clear words over abbreviations
        private static void AddConfiguration(HostBuilderContext ctx, IConfigurationBuilder bldr)
        {
            //The line of code below gets rid of the need for the AppConfiguration file
            //Note that you don't need to do these stuff in your normal projects, the knowledge however can be userful in future
            bldr.Sources.Clear();

            //The line of code below declares the location of a configuration file we intend to use in this project seeing as we deleted the scaffolded one in favor of learning how to build things from the ground up.
            bldr.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                //Adding the ".AddEnvironmentVariables()" declaration is especially useful when you're dealing with cloud deployment to define configuration pieces
                //This basically builds a configuration based on the sources you're putting into it,
                //The order matters when chaining these methods, this "config.json" file can be a default that is overrideable by the ".AddEnvironmentVariables()" method especially when the method has it's own name or path
                .AddEnvironmentVariables();
        }
    }
}
