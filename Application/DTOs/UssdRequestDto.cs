using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UssdRequestDto
    {
        public required string SessionId { get; set; }
        public required string ServiceCode { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Text { get; set; }
      
    }
}
