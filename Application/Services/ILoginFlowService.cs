﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ILoginFlowService
    {
        Task<string> ProcessLoginFlowAsync(string phoneNumber, string[] inputParts);
    }
}
