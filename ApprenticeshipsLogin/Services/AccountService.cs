using ApprenticeshipsLogin.Database;
using ApprenticeshipsLogin.Exceptions;
using ApprenticeshipsLogin.Models;
using ApprenticeshipsLogin.Models.Commands;
using ApprenticeshipsLogin.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApprenticeshipsLogin.Services
{
    public class AccountService : IAccountService
    {
        private readonly IFakeDatabase _context;
        private readonly IPasswordHasher<DbUsers> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        public AccountService(IPasswordHasher<DbUsers> passwordHasher, AuthenticationSettings authenticationSettings, IFakeDatabase context)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }
        public string GenerateJwt(LoginCommand dto)
        {
            var users = _context.GetUsers();

            var user = users.FirstOrDefault(u => u.Email == dto.Email);

            if (user == null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.Surname}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        public List<string> GetAllUsers()
        {
            var listOfEmails = new List<string>();
            var users = _context.GetUsers();
            
            foreach (var user in users)
            {
                listOfEmails.Add(user.Email);
            }

            return listOfEmails;
        }

        public void RegisterUser(RegisterUserCommand dto)
        {
            var newUser = new DbUsers()
            {
            Email = dto.Email,
            FirstName = dto.FirstName,
            Surname = dto.Surname,
            Role = dto.Role
            };

            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;

            _context.AddUser(newUser);
        }
    }
}
