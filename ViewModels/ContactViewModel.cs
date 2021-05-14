using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.ViewModels
{
    public class ContactViewModel
    {
        //View models create a convenient way to expose properties of a model (which is to be used in a view) without exposing the entire model to the view
        //The View model is like a bridge between the view and the model in terms of exchange of data.
        //The View model is an offshoot of the model classes and the view model is meant to interact with the view while acting as representative of the model.
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
