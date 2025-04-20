using Application.Commands;
using Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly ILoginFlowService _loginFlowService;

        public LoginCommandHandler( ILoginFlowService loginFlowService)
        {
            _loginFlowService = loginFlowService ?? throw new ArgumentNullException(nameof(loginFlowService));
        }
        public Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {

            if (request.FlowRequestDto == null)
            {
                throw new ArgumentNullException(nameof(request.FlowRequestDto));
            }

            var phoneNumber = request.FlowRequestDto.PhoneNumber;

            var inputParts = request.FlowRequestDto.InputParts;

            if (string.IsNullOrEmpty(phoneNumber))
            {
                throw new ArgumentException("Phone number cannot be null or empty.", nameof(phoneNumber));
            }
            if (inputParts == null || inputParts.Length == 0)
            {
                throw new ArgumentException("Input parts cannot be null or empty.", nameof(inputParts));
            }

            var result = _loginFlowService.ProcessLoginFlowAsync(phoneNumber, inputParts);

            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            return result;
        }
    }
}
