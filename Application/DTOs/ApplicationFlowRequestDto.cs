using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ApplicationFlowRequestDto
    {
        public required string PhoneNumber { get; set; }
        public required string[] InputParts { get; set; }
    }
}
