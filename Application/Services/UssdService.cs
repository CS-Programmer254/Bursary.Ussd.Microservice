using Application.Commands;
using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UssdService : IUssdService
    {
        private readonly IMediator _mediator;
        public UssdService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        public async Task<string> HandleUssdRequestAsync(UssdRequestDto request)
        {
            try
            {
                var text = request.Text ?? string.Empty;

                var response = string.Empty;

                // Check if the request is a new session or a continuation of an existing session
                // Split the text by the '*' character to get the input parts
                // This is a common way to handle USSD input, where each level of the menu is separated by '*'
                var inputParts = text.Split('*');

                if (string.IsNullOrEmpty(text))
                {
                    return "CON Welcome to Rattansi Bursary\n1. Register\n2. Login\n3. Verify Account\n0. Exit";

                }
                else if (text == "0")
                {
                    response = "Thank you for using the Rattansi Bursary Application System. Goodbye!";

                }
                // Registration flow
                if (inputParts[0] == "1")
                {
                    var ApplicationFlowRequest = new ApplicationFlowRequestDto
                    {
                        PhoneNumber = request.PhoneNumber,
                        InputParts = inputParts
                    };

                    var RegistrationFlowResult = await _mediator.Send(
                        new RegisterUserCommand
                        {
                            FlowRequestDto = ApplicationFlowRequest
                        }
                    );

                    return RegistrationFlowResult;


                }

                // Login flow
                if (inputParts[0] == "2")
                {
                    var ApplicationFlowRequest = new ApplicationFlowRequestDto
                    {
                        PhoneNumber = request.PhoneNumber,
                        InputParts = inputParts
                    };
                     
                    var loginFlowResult = await _mediator.Send(
                        new LoginCommand
                        {
                            FlowRequestDto = ApplicationFlowRequest
                        }
                    );

                    return loginFlowResult;
                }

                // Verification flow
                if (inputParts[0] == "3")
                {
                    return await ProcessVerificationFlow(inputParts, request.PhoneNumber);
                }
                // Invalid option
                return "END Invalid option. Please start again.";

            }
            catch (Exception ex)
            {
                return "END System error ${ex}. Please try again later.";
                
            }

        }

       
        private static async Task<string> ProcessVerificationFlow(string[] inputParts, string phoneNumber)
        {
            var currentStep = inputParts.Length;

            if (currentStep == 1)
            {
                return "CON Enter OTP sent to your phone:";
            }

            try
            {
                //var verifyResult = await _authService.VerifyAccountAsync(phoneNumber, inputParts[1]);

                //if (!verifyResult.Success)
                //{
                //    return $"END Verification failed: {verifyResult.Message}\nDial *384*3# to try again.";
                //}

                return "END Account verified successfully! You can now login.";
            }
            catch (Exception ex)
            {
  
                return "END Verification error. Please try again.";
            }
        }

    }
}
