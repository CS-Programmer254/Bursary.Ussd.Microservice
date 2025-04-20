using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class HandleUssdCommand:IRequest<string>
    {
       public required UssdRequestDto UssdRequest { get; set; }  
    }
}
