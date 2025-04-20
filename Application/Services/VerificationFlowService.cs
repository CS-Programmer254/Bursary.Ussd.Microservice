using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class VerificationFlowService : IVerificationFlowService
    {
        public async Task<bool> VerifyAccountAsync(string phoneNumber, string[] inputParts)
        {
            throw new NotImplementedException();
        }
    }
}
