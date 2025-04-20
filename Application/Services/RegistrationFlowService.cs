using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RegistrationFlowService : IRegistrationFlowService
    {
        private readonly HttpClient _httpClient;

        public RegistrationFlowService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> ProcessRegistrationFlowAsync(string phoneNumber, string[] inputParts)
        {
            try
            {
                var currentStep = inputParts.Length;
                Console.WriteLine($"Step: {currentStep}, InputParts: {string.Join(",", inputParts)}, Phone: {phoneNumber}");

                return currentStep switch
                {
                    1 => "CON Enter First Name:",
                    2 => "CON Enter Middle Name:",
                    3 => "CON Enter Last Name:",
                    4 => "CON Enter National ID Number:",
                    5 => "CON Enter Email Address:",
                    6 => "CON Enter Admission Number:",
                    7 => "CON Select Department:\n1. SCI001 - Computer Science\n2. SCI002 - Information Technology",
                    8 => inputParts[7] switch
                    {
                        "1" => "CON Select your Course:\n1. BSc. Computer Science\n2. BSc. Computer Technology",
                        "2" => "CON Select your Course:\n1. BSc. Information Technology\n2. BSc. Information Systems\n3. BSc. Information Communication Technology",
                        _ => "CON Invalid department. Select:\n1. SCI001 - Computer Science\n2. SCI002 - Information Technology"
                    },
                    9 => "CON Enter Current Year (e.g., 1, 2, 3, 4):",
                    10 => "CON Select Gender:\n1. Male\n2. Female",
                    11 => inputParts.Length <= 10 || string.IsNullOrEmpty(inputParts[10])
                        ? "CON Invalid input. Select Gender:\n1. Male\n2. Female"
                        : inputParts[10] switch
                        {
                            "1" => "CON Create Password:",
                            "2" => "CON Create Password:",
                            _ => "CON Invalid gender. Select:\n1. Male\n2. Female"
                        },
                    12 => await CompleteRegistration(inputParts, phoneNumber),
                    _ => "END Invalid registration sequence"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error at step {inputParts.Length}: {ex.Message}");
                return "END An error occurred. Please try again.";
            }
        }
        private async Task<string> CompleteRegistration(string[] inputParts, string phoneNumber)
        {
            try
            {
                if (inputParts.Length < 12 || string.IsNullOrEmpty(inputParts[11]))
                {
                    return "END Invalid input. Please try again.";
                }
                string departmentId = inputParts[7] == "1" ? "SCI001" : "SCI002";

                string courseName = departmentId switch
                {
                    "SCI001" => inputParts[8] switch
                    {
                        "1" => "BSc. Computer Science",
                        "2" => "BSc. Computer Technology",
                        _ => throw new ArgumentException("Invalid course selection")
                    },
                    "SCI002" => inputParts[8] switch
                    {
                        "1" => "BSc. Information Technology",
                        "2" => "BSc. Information Systems",
                        "3" => "BSc. Information Communication Technology",
                        _ => throw new ArgumentException("Invalid course selection")
                    },
                    _ => throw new ArgumentException("Invalid department")
                };

                string gender = inputParts[10] == "1" ? "Male" : "Female";

                var registration = new
                {
                    firstName = inputParts[1],
                    middleName = inputParts[2],
                    lastName = inputParts[3],
                    nationalIdentificationNumber = inputParts[4],
                    emailAddress = inputParts[5],
                    admissionNumber = inputParts[6],
                    departmentId = departmentId,
                    courseName = courseName,
                    currentYear = inputParts[9],
                    gender = gender,
                    password = inputParts[11],
                    phoneNumber = phoneNumber,
                    role = "ROLE_STUDENT"
                };

                var json = JsonSerializer.Serialize(registration);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                Console.WriteLine($"Sending registration data: {json}");
           
                var response = await _httpClient.PostAsync("http://localhost:8084/api/v1/auth/register", content);
                Console.WriteLine($"API Response Status: {response.StatusCode}");


                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                  
                    Console.WriteLine($"API Error Response: {error}");
                  
                    return $"END Registration failed: {error}";
                }

                return "END Registration successful! Check SMS for OTP to verify your account. Dial *384*3# to verify.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CompleteRegistration Error: {ex.Message}");
               
                return $"END Registration failed. Error: {ex.Message}";
            }
        }
    }
}
