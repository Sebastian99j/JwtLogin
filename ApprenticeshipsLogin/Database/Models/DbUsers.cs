namespace ApprenticeshipsLogin.Models
{
    public class DbUsers
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DbUserRoles Role { get; set; }
    }
}
