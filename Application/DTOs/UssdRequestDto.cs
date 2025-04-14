using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UssdRequestDto
    {
        public string SessionId { get; set; }
        public string ServiceCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Text { get; set; }
        //public string UserId { get; set; }
        //public string UserName { get; set; }
        //public string UserLanguage { get; set; }
        //public string UserLocation { get; set; }
        //public DateTime RequestTime { get; set; }
       
    }
}
