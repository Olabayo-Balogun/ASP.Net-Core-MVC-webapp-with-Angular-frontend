using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    //It's important to create a Context class and inherit from DbContext which will help you execute your queries to the database/data table 
    public class DutchContext : DbContext
    {
        //The property below is what is used to issue queries to the Products class/data table. It can also be used to add to the products in the database  
        //By convention, the name of these properties is the plural form of the class
        //We didn't create a DbSet for the "OrderItem" class because the "Order" class has a strong relationship with the "OrderItem" class and we don't need to query the "OrderItem" separately
        //You can choose to create a DbSet for it, it's totally optional
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
