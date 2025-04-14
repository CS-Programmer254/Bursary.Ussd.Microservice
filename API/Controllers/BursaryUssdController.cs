using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BursaryUssdController : ControllerBase
    {
        private readonly IUssdService _ussdService;
        public BursaryUssdController( IUssdService ussdService)
        {
            _ussdService = ussdService ?? throw new ArgumentNullException(nameof(ussdService));
        }
        public async Task<IActionResult> HandleUssd([FromBody] UssdRequestDto request)
        {
            if(request == null)
            {
                return BadRequest("Invalid request");
            }
        
            var response = await _ussdService.HandleUssdRequestAsync(request);

            return Ok(response);
        }
    }
}
