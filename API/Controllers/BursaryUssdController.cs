using Application.Commands;
using Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BursaryUssdController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BursaryUssdController(IMediator mediator)
        {
           
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("handle")]
        [Consumes("application/x-www-form-urlencoded")] 
        public async Task<IActionResult> HandleUssd()
        {
            var formData = await Request.ReadFormAsync();

            if (formData == null || formData.Count == 0)
            {
                return BadRequest("Invalid request data.");
            }
            var request = new UssdRequestDto
            {
                SessionId = formData["sessionId"],
                ServiceCode = formData["serviceCode"],
                PhoneNumber = formData["phoneNumber"],
                Text = formData["text"]
            };

            var command = new HandleUssdCommand { UssdRequest = request };

            var response = await _mediator.Send(command);

            return Content(response, "text/plain");
        }
    }
}
