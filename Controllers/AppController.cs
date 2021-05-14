using DutchTreat.Services;
using DutchTreat.ViewModels;
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
        //Private fields are always named with the "_" prefix (by convention)
        private readonly IMailService _mailService;

        //This constructor is used to declare and make use of services in the "Startup" class
        //You should as usual create a private field for declarations like these.
        public AppController(IMailService mailService)
        {
            _mailService = mailService;
        }

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

        //The "object" parameter written in the line of code below isn't specific enough so it can't be matched to the database or any model
        //Code
        //public IActionResult Contact(object model)

        //The ContactViewModel specifies how the action can interact with the model/database
        //Whatever inputs in the form will matched to the properties in the "ContactViewModel" as long as their names match
        //The casing of the property names and the "name" attribute on the form doesn't need to the same, uppercase or lowercase it'll be recognizeable if the names are spelt in the same way
        //If you add a breakpoint to the "return View()" line of code and run the program, if you check the "model" parameter name it'll show you a snapshot view of the information from the contact form passed directly into the properties of the ContactViewModel
        public IActionResult Contact(ContactViewModel model)
        {
            //The code just below is the default purpose of an action
            //Code
            //return View();

            //The declaration below is used to control what happens if the input matches the requirement of the ViewModel class the action is assigned to.
            //In this case, the Contact action is assigned to the ContactViewModel so this ModelState is used to control what happens if the input matches the requirement of the ContactViewModel
            if (ModelState.IsValid)
            {
                //We're demoing the sending of the mail response (from the contact form) below
                _mailService.SendMessage("shawn@wildermuth.com", model.Subject, $"From: {model.Name} -{model.Email}, Message: {model.Message}");

                //We want to get a response after we've sent the mail and we're writing a code below to demo it. Note that there are better ways to do this.
                ViewBag.UserMessage = "Mail Sent";

                //We write the code below to clear the form once it's sent so we have a blank form after    
                ModelState.Clear();
            }
            //There is really no need to add the "else" block because the view page already handles invalid ModelState 
            //else
            //{
                
            //}

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
