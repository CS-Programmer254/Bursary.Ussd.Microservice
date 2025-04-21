using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FormatPhoneNumberService : IFormatPhoneNumberService
    {
        public string? FormatPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return null;
            }

            // Remove any whitespace, dashes, or other non-digit characters (except +)
            string cleanedNumber = Regex.Replace(phoneNumber.Trim(), "[^0-9+]", "");

            // Handle various input formats
            // Case 1: +254769860885
            if (cleanedNumber.StartsWith("+254") && cleanedNumber.Length == 12)
            {
                return cleanedNumber;
            }
            // Case 2: 254769860885
            else if (cleanedNumber.StartsWith("254") && cleanedNumber.Length == 12)
            {
                return $"+{cleanedNumber}";
            }
            // Case 3: 0769860885 or 769860885
            else if (cleanedNumber.Length == 10 && cleanedNumber.StartsWith("0") ||
                     cleanedNumber.Length == 9 && Regex.IsMatch(cleanedNumber, @"^[0-9]{9}$"))
            {
                // Remove leading 0 if present, then prepend +254
                string digits = cleanedNumber.StartsWith("0") ? cleanedNumber.Substring(1) : cleanedNumber;
                return $"+254{digits}";
            }

            // Invalid format
            return null;
        }
    }
}
