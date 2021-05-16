using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DutchTreat.Data.Entities;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Data
{
    //We're creating an interface for this because mostly because we may need to carry out unit tests and may need to create a mock version of the repository
  public class DutchRepository : IDutchRepository
  {
    private readonly DutchContext _ctx;
    private readonly ILogger<DutchRepository> _logger;

        //As usual we have to bring in the DutchContext
        public DutchRepository(DutchContext ctx, ILogger<DutchRepository> logger)
        {
            _ctx = ctx;
        }

        //public DutchRepository(DutchContext ctx, ILogger<DutchRepository> logger) 
        //{
        //  _ctx = ctx;
        //  _logger = logger;
        //}

        //This method will return all the products as a list
        public IEnumerable<Product> GetAllProducts()
        {
            return _ctx.Products
                        .OrderBy(p => p.Title)
                        .ToList();
        }

        //public IEnumerable<Product> GetAllProducts()
        //{
        //  try
        //  {
        //    _logger.LogInformation("GetAllProducts was called...");

        //    return _ctx.Products
        //               .OrderBy(p => p.Artist)
        //               .ToList();

        //  }
        //  catch (Exception ex)
        //  {
        //    _logger.LogError($"Failed to get all products: {ex}");
        //    return null;
        //  }
        //}

        //We're trying to get the list of product by category, but we need the user to input the category
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
          return _ctx.Products
                     .Where(p => p.Category == category)
                     .ToList();
        }

        //This will respond to the "SaveAll" in the interface
        public bool SaveAll()
        {
            //This works when the number of rows that has been changed is greater than 0
          return _ctx.SaveChanges() > 0;
        }
      }
}
