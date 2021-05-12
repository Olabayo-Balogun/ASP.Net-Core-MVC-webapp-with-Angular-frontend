using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    //Your MVC controllers must inherit from "Controller"
    //The "Controller" class uses "Microsoft.AspNetCore.Mvc" using statements
    //The word before the "Controller" is assumed to be the name of the controller
    //Using the code below we can conclude that the name of the controller below is "App"
    //The machine will only see "App" but because of the "Controller behind it, the machine knows that "App" is a controller
    //The view folder that responds to this controller must bear the same name, in this case, the view must be named "App"
    public class AppController : Controller
    {
        //The format of code below is used to map data to a view and return it.
        //IActionResult is used in these cases
        //The view that responds to this method below must share the same name as this view
        //In this we know that the view must be named "Index"
      public IActionResult Index()
        {
            //throw new InvalidProgramException("Bad things happen to good developers");
            return View();
        }
    }
}
