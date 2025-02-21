public class UserWithRolesAndPermissions
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public List<RoleDTO> Roles { get; set; }
    public List<UserClaimDTO> Claims { get; set; }
    public List<String> RoleClaims { get; set; }

}