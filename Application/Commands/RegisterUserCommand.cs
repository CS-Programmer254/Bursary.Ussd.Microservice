using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class RegisterUserCommand:IRequest<string>
    {
        public required ApplicationFlowRequestDto FlowRequestDto;
    }
}
