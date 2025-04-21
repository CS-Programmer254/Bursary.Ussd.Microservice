using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;
using Application.Services;
using MediatR;

namespace Application.CommandHandlers
{
    public class VerifyAccountCommandHandler : IRequestHandler<VerifyAccountCommand, string>
    {
        private readonly IVerificationFlowService _verificationFlowService;
        public VerifyAccountCommandHandler(IVerificationFlowService verificationFlowService)
        {
            _verificationFlowService= verificationFlowService ?? throw new ArgumentNullException(nameof(verificationFlowService));
        }
        public Task<string> Handle(VerifyAccountCommand request, CancellationToken cancellationToken)
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

            var result = _verificationFlowService.VerifyAccountAsync(phoneNumber, inputParts);

            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            return result;

        }
    }
}
