using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.ViewModels
{
    public class ContactViewModel
    {
        //View models create a convenient way to expose properties of a model (which is to be used in a view) without exposing the entire model to the view
        //The View model is like a bridge between the view and the model in terms of exchange of data.
        //The View model is an offshoot of the model classes and the view model is meant to interact with the view while acting as representative of the model.

        //Attributes like "[Required]", "[EmailAddress]" etc are what is known as validation.
        //Properties that have the  "[Required]" attribute cannot be left empty, the program expects that the user will input something when submitting data for that property even if it is within a form with other input declarations

        //These attributes use the "using System.ComponentModel.DataAnnotations;" namespace
        //These attribute help ensure that the input coming in conforms to the style, format and rules of what is expected from the property.
        //You can add "[RegularExpression()]" if you want to create more bespoke conditions for a property. It is commonly used for "Username", "EmailAddress" and "Password" properties. 

        //It is important to add ModelState to the action (inside a controller class) for the class you use the validation on 
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        //[MaxLength(250)]

        //The MaxLength attribute allows for multiple parameters which allows you to customize the error message that the user gets when they break the rules of the attribute declared in the ViewModel, in this case, if the MaxLength attribute is disobeyed, the user will the "Too long" as a response from the computer
        //Please note that the error message will only show if the "asp-validation-summary" is declared in the razor page (most preferably inside a "div" tag)
        [MaxLength(250, ErrorMessage ="Too long")]
        public string Message { get; set; }
    }
}
