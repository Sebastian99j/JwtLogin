using ApprenticeshipsLogin.Services.Interfaces;
using MediatR;

namespace ApprenticeshipsLogin.Models.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IAccountService _service;

        public LoginCommandHandler(IAccountService service)
        {
            _service = service;
        }

        public Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = _service.GenerateJwt(request);

            return Task.FromResult(result);
        }
    }
}
