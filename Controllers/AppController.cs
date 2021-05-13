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
            //The throw code simply returns the InvalidProgramException page
            //Don't uncommented the code or this project won't build.
            //throw new InvalidProgramException("Bad things happen to good developers");
            return View();
        }

        //The attribute below declares the routing parameter and overides the default routing parameter
        //Normally everything in this controller falls under the "App" route as such any action should be prefixed by "App/"
        //with the "HttpGet" attribute you can change the routing pattern.
        //Now the routing pattern for contact isn't "App/Contact", it's simply "contact"
        [HttpGet("contact")]
        public IActionResult Contact()
        {
            //It is possible to declare the title of the page in the method, that is what is done here
            //It isn't advisable to do this as you'll have to repeat it in every overload
            //It is better to declare this in the view page
            //ViewBag.Title = "Contact Us";

            return View();            
        }

        //This action is meant to respond to the input of the "Contact.cshtml" form
        //The "HttpPost" attributes helps the programme to know which action the "Contact.cshtml" form is calling
        [HttpPost("contact")]
        public IActionResult Contact(object model)
        {
            return View();
        }

        public IActionResult About()
        {
            //It is possible to declare the title of the page in the method, that is what is done here
            ViewBag.Title = "About Us";

            return View();
        }
    }
}
