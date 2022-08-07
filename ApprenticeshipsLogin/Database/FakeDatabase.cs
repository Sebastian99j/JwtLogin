using ApprenticeshipsLogin.Models;
using Microsoft.AspNetCore.Identity;

namespace ApprenticeshipsLogin.Database
{
    public class FakeDatabase
    {
        public List<DbUsers> Users { get; set; } = new List<DbUsers>();
        public List<DbUserRoles> Roles { get; set; } = new List<DbUserRoles>();
        private readonly IPasswordHasher<DbUsers> _passwordHasher;

        public FakeDatabase(IPasswordHasher<DbUsers> passwordHasher)
        {
            _passwordHasher = passwordHasher;

            Seed();
        }

        public List<DbUsers> GetUsers()
        {
            return this.Users;
        }

        public void AddUser(DbUsers User)
        {
            this.Users.Add(User);
        }

        public void DeleteUser(DbUsers User)
        {
            try
            {
                var removeElement = this.Users.IndexOf(User);
                Users.RemoveAt(removeElement);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void Seed()
        {
            Roles = new List<DbUserRoles>()
            {
                new DbUserRoles()
                {
                    Id = 1,
                    Name = "Admin"
                },
                new DbUserRoles()
                {
                    Id = 2,
                    Name = "Maintainer"
                },
                new DbUserRoles()
                {
                    Id = 3,
                    Name = "User"
                }
            };

            Users = new List<DbUsers>()
            {
                new DbUsers()
                {
                Id = 1,
                Email = "admin@gmail.com",
                FirstName = "Jan",
                Surname = "Kowalski",
                Role = Roles[0]
                },
                new DbUsers()
                {
                Id = 1,
                Email = "maintainer@gmail.com",
                FirstName = "Zenek",
                Surname = "Martyniuk",
                Role = Roles[1]
                },
                new DbUsers()
                {
                Id = 1,
                Email = "user@gmail.com",
                FirstName = "Jakub",
                Surname = "Bonifacy",
                Role = Roles[2]
                }
            };

            Users[0].PasswordHash = _passwordHasher.HashPassword(Users[0], "admin");
            Users[1].PasswordHash = _passwordHasher.HashPassword(Users[1], "maintainer");
            Users[2].PasswordHash = _passwordHasher.HashPassword(Users[2], "user");
        }
    }
}
