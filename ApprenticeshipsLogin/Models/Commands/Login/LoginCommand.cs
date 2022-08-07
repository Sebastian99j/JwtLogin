using MediatR;

namespace ApprenticeshipsLogin.Models.Commands
{
    public class LoginCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
