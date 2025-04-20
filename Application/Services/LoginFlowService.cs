using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LoginFlowService : ILoginFlowService
    {
        public LoginFlowService()
        {
           
        }

        public async Task<string> ProcessLoginFlowAsync(string phoneNumber, string[] inputParts)
        {
            var currentStep = inputParts.Length;

            if (currentStep == 1)
            {
                return "CON Enter Your National ID:";
            }

            try
            {
                //var loginResult = await _authService.LoginAsync(phoneNumber, inputParts[1]);

                //if (!loginResult.Success)
                //{
                //    if (loginResult.RequiresVerification)
                //    {
                //        return "CON Account not verified. Dial *384*3# to verify.\n0. Main Menu";
                //    }
                //    return $"END Login failed: {loginResult.Message}";
                //}

                return "CON Login successful!\n1. Apply Bursary\n2. Check Status\n0. Exit";
            }
            catch (Exception ex)
            {
                return "END Login error. Please try again.";
            }

        }


    }
    
}
