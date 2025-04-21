using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class VerifyAccountCommand : IRequest<string>
    {
        public required ApplicationFlowRequestDto FlowRequestDto;
    }
}
