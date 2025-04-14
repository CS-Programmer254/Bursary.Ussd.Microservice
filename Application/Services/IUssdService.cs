using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUssdService
    {
        Task<string> HandleUssdRequestAsync(UssdRequestDto request);
    }
}
