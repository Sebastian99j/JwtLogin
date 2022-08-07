using MediatR;

namespace ApprenticeshipsLogin.Models.Commands
{
    public class RegisterUserCommand : IRequest
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DbUserRoles Role { get; set; }
    }
}
