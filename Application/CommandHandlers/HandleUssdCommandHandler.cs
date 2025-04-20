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
    public class HandleUssdCommandHandler : IRequestHandler<HandleUssdCommand, string>
    {

        private readonly IUssdService _ussdService;

        public HandleUssdCommandHandler( IUssdService ussdService ,IMediator mediator)
        {
            _ussdService = ussdService ?? throw new ArgumentNullException(nameof(ussdService));

        }
        public Task<string> Handle(HandleUssdCommand command, CancellationToken cancellationToken)
        {
            if (command== null)
                throw new ArgumentNullException(nameof(command));

            var request = command.UssdRequest;

            return _ussdService.HandleUssdRequestAsync(request);
    
        }
    }
}
