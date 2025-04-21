using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services
{
    public class VerificationFlowService : IVerificationFlowService
    {
        private readonly HttpClient _httpClient;

        private readonly IFormatPhoneNumberService _formatPhoneNumberService;

        public VerificationFlowService(HttpClient httpClient, IFormatPhoneNumberService formatPhoneNumberService)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            _formatPhoneNumberService = formatPhoneNumberService ?? throw new ArgumentNullException(nameof(formatPhoneNumberService));

        }
        public async Task<String> VerifyAccountAsync(string phoneNumber, string[] inputParts)
        {
            return await ProcessVerificationFlowAsync(inputParts, phoneNumber);

        }

        private async Task<string> ProcessVerificationFlowAsync(string[] inputParts, string phoneNumber)
        {
            var currentStep = inputParts.Length;

            if (currentStep == 1)
            {
                return "CON Enter OTP sent to your phoner:";
            }

            if ( string.IsNullOrEmpty(inputParts[1]))
            {
                return "END Invalid OTP input. Dial *384*9916# and choose option 3 to try again.";
            }

            try
            {
                
                Console.WriteLine($"Sending verification data: {phoneNumber}");



                String phone = phoneNumber;

                var verificationPayload = new
                {
                    phoneNumber =phone,
                    otp = inputParts[1]
                };

                var json = JsonSerializer.Serialize(verificationPayload);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                Console.WriteLine($"Sending verification data: {json}");

                var response = await _httpClient.PostAsync("http://localhost:8084/api/v1/auth/verify-otp", content);

                Console.WriteLine($"API Account Verification Response Status: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    return $"END Account verified successfully! You can now login.{response}";
                }

                var errorContent = await response.Content.ReadAsStringAsync();

                return $"END Verification failed: {errorContent}\nDial *384*9916#  and choose option 3 to try again.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Verification error: {ex.Message}");

                return "END Verification error. Please try again.";
            }
        }
    }
}
