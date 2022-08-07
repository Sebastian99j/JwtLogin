using ApprenticeshipsLogin.Models.Commands;

namespace ApprenticeshipsLogin.Services.Interfaces
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserCommand dto);
        string GenerateJwt(LoginCommand dto);
        List<string> GetAllUsers();
    }
}
