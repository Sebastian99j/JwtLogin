using ApprenticeshipsLogin.Services.Interfaces;
using MediatR;

namespace ApprenticeshipsLogin.Models.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Unit>
    {
        private readonly IAccountService _service;

        public RegisterUserCommandHandler(IAccountService service)
        {
            _service = service;
        }

        public Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            _service.RegisterUser(request);

            return Task.FromResult(Unit.Value);
        }
    }
}
