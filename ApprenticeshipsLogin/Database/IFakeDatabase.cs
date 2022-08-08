using ApprenticeshipsLogin.Models;

namespace ApprenticeshipsLogin.Database
{
    public interface IFakeDatabase
    {
        List<DbUserRoles> Roles { get; set; }
        List<DbUsers> Users { get; set; }

        void AddUser(DbUsers User);
        void DeleteUser(DbUsers User);
        List<DbUsers> GetUsers();
    }
}