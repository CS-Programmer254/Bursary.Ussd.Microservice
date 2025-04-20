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
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
    {
        private readonly IRegistrationFlowService _registrationFlowService;

        public RegisterUserCommandHandler(IRegistrationFlowService registrationFlowService)
        {
            _registrationFlowService = registrationFlowService ?? throw new ArgumentNullException(nameof(registrationFlowService));

        }
        public Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
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

            var result = _registrationFlowService.ProcessRegistrationFlowAsync(phoneNumber, inputParts);

            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            return result;

        }
    }
}
