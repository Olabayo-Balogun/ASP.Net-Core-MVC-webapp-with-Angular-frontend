using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Services
{
    public class NullMailService : IMailService
    {
        private readonly ILogger<NullMailService> _logger;

        //In order to log the response from the contact form we need to use the logger.
        //The ILogger requires the "using Microsoft.Extensions.Logging;" statement
        //It is important to create a private field for constructors like these by creating a private field for the name parameter of the class you're injecting as a parameter into the constructor
        public NullMailService(ILogger<NullMailService> logger)
        {
            _logger = logger;
        }
        public void SendMessage(string to, string subject, string body)
        {
            //This part is where the code that logs the message will be written 
            //The result is supposed to display in console since we don't have the entire infrastucture worked out with SendGrid
            _logger.LogInformation($"To: {to} Subject: {subject} Body: {body}");
        }
    }
}
