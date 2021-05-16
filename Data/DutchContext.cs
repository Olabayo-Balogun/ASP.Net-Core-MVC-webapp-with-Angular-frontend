using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    //It's important to create a Context class and inherit from DbContext which will help you execute your queries to the database/data table 
    public class DutchContext : DbContext
    {
        //The private field and the constructor is built to enable us get a connection string as a parameter for our SQL
        private readonly IConfiguration _config;

        public DutchContext(IConfiguration config)
        {
            _config = config;
        }

        //public DutchContext(DbContextOptions<DutchContext> options) : base(options)
        //{

        //}

        //The property below is what is used to issue queries to the Products class/data table. It can also be used to add to the products in the database  
        //By convention, the name of these properties is the plural form of the class
        //We didn't create a DbSet for the "OrderItem" class because the "Order" class has a strong relationship with the "OrderItem" class and we don't need to query the "OrderItem" separately
        //You can choose to create a DbSet for it, it's totally optional
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        //The code below is used to enable us create and update database using entity framework core
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //Because you need the connection string as a parameter for the code below you have to create a constructor above and call in some other useful parameters
            //The parameter passed in will involve looking for the "config.json" file and then the "ConnectionStrings" value-pair that we declared in that file
            //If our key value-pair is deeply nested, we would have to use a lot of ":" to get to it in the same way we use the "/" to get to the path of a document
            //The purpose of specifying the path is to pull the value of the "DutchContextDb" which was specified in the "config.json" file. The value of that key is a string that lets this method know where to find the database it'll be using
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:DutchContextDb"]);
        }

    //The code below can also specify the relationship between entities and the database thus enabling them to map to one another
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //The code below can be used to specify properties as it is being created in the database
            //Code
            //modelBuilder.Entity<Product>()
            //    .Property(p => p.Title)
            //    .HasMaxLength(50);

            //We're using the block of code below to seed data into our model (which has a table in the database)
            //".HasData" is what makes this possible after "modelBuilder" maps to the model class
            modelBuilder.Entity<Order>()
                .HasData(new Order()
                {
                    Id = 1,
                    OrderDate = DateTime.UtcNow,
                    OrderNumber = "12345"
                });

            //We seed this data above by opening our command window (Developer powershell), navigating to the data folder and typing the command "dotnet ef migrations add SeedDate".
            //Note that the "SeedDate" is simply a name for the migration
;        }
    }
}
