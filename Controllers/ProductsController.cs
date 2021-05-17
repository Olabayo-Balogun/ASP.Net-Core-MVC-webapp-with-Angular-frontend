using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
  [Route("api/[Controller]")]
  [ApiController]
  //The attribute below helps tell the project that we will always be returning data in JSON form
  [Produces("application/json")]
  public class ProductsController : ControllerBase
  {
    private readonly IDutchRepository _repository;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IDutchRepository repository, ILogger<ProductsController> logger)
    {
      _repository = repository;
      _logger = logger;
    }

    //The attribute "[HttpGet]" is more important than the name of the method, although, the method name should correspond with the attribute name by convention
    [HttpGet]
    //The two attributes below also help specify the type of response to expect from our code. You can add as many response types as possible. 
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    
    //The ActionResult<IEnumerable<Product>> helps us specify how we want the product to be returned (which is as a list) and it is especially convenient especially when we're exposing an API to the public and we want to control how it is used.
    public ActionResult<IEnumerable<Product>> Get()
    {
      try
      {
        //The "Ok" is used to return an "Ok" status report
        return Ok(_repository.GetAllProducts()); 
      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to get products: {ex}");
        return BadRequest("failed to get products");
      }
    }
  }
}
