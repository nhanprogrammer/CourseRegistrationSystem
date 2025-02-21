public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<UserClaim> UserClaims { get; set; }
    public ICollection<UserLogin> UserLogins { get; set; }
}