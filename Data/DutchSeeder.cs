using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _ctx;
        private readonly IWebHostEnvironment _hosting;

        //The "IWebHostEnvironment" is used to understand which environment the project is running in
        public DutchSeeder(DutchContext ctx, IWebHostEnvironment hosting)
        {
            _ctx = ctx;
            _hosting = hosting;
        }

        public void Seed()
        {
            //The line of code just below ensures there's a database to begin with, it reduces the chances of querying a database that doesn't exist. 
            //If the database doesn't exist, that method will create it
            _ctx.Database.EnsureCreated();

            //The if statement just below executes if there are no products in the database
            if (!_ctx.Products.Any())
            {
                // The "Path.Combine" is used to get the root of the environment and add it to the path (that we know) for the "art.json" file
                //Note that the "art.json" file contains seed data for art products
                var file = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(file);
                //The "JsonSerializer" is used to break down the "art.json" file into something that's consistent with what we can easily read then add it into the product model (which has a table in the database)
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);

                //AddRange is used because we have a collection of products (which we got from the deserialized "art.json" file).
                //We specify "Products" below so it knows where the data is going
                _ctx.Products.AddRange(products);

                //We're trying to create a new order here
                var order = new Order()
                {
                    OrderDate = DateTime.Today,
                    OrderNumber = "10000",
                    //The "Items" is a list because an order can have more than one OrderItem
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            //This gets the first product from the database and makes it our order
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                };

                //      var order = _ctx.Orders.Where(o => o.Id == 1).FirstOrDefault();
                //      if (order != null)
                //      {
                //          order.Items = new List<OrderItem>()
                //{
                //  new OrderItem()
                //  {
                //    Product = products.First(),
                //    Quantity = 5,
                //    UnitPrice = products.First().Price
                //  }
                //};
                _ctx.Orders.Add(order);
            }

           

             //This line of code saves all the changes we've made above
             _ctx.SaveChanges();
        }
    }
}

