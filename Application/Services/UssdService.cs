using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UssdService : IUssdService
    {
        public async Task<string> HandleUssdRequestAsync(UssdRequestDto request)
        {
            var text = request.Text ?? string.Empty;

            var inputParts = text.Split('*');

            var response = string.Empty;

            if (string.IsNullOrEmpty(text))
            {

                response = "CON Welcome to the Bursary Application\n1. Apply Bursary Now\n2. Check Existing Bursary Applications\n0. Exit";

            }
            else if (text == "0")
            {
                response = "Thank you for using the Rattansi Bursary Application App. Goodbye!";
               
            }
            else if (text == "1")
            {
                response = "CON Please enter your full name:";
            }
            else if (inputParts.Length == 2 && inputParts[0]=="1")
            {
                response = $"CON Please enter your ID number:";
            }
            else if (inputParts.Length == 3 && inputParts[0] == "1" && inputParts[1] == "1")
            {
                response = $"CON Please enter your email address:";
            }
            else if (inputParts.Length == 4 && inputParts[0] == "1" && inputParts[1] == "1" && inputParts[2] == "1")
            {
                response = $"CON Please enter your phone number:";
            }
            else if (inputParts.Length == 5 && inputParts[0] == "1" && inputParts[1] == "1" && inputParts[2] == "1" && inputParts[3] == "1")
            {
                // validate applicant details
                // prepare payload for the API
                // call the API to save the application
                // handle the response from the API
                response = $"END Thank you for applying for the Bursary. Your application has been submitted.";

            }else if (text == "2")
            {
                response = "CON Please enter your ID number to check the status of your application:";
            }
            else if (inputParts.Length == 2 && inputParts[0] == "2")
            {
                // validate ID number
                // call the API to check the status of the application
                // handle the response from the API
                response = $"END Your application status is: Approved.";
            }
            else
            {
                response = " END Invalid option. Please try again!";
            }

            return response;
        }
    }
}
