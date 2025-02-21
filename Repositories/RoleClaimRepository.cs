using Microsoft.EntityFrameworkCore;

public class RoleClaimRepository
{
    private readonly SchoolContext _context;

    public RoleClaimRepository(SchoolContext context)
    {
        _context = context;
    }

public async Task<List<RoleClaimDTO>> GetAllUsersWithRolesAndPermissionsAsync(List<int> roleIds)
{
    return await (from rc in _context.RoleClaims
                  join r in _context.Roles on rc.RoleId equals r.Id
                  where roleIds.Contains(rc.RoleId)
                  select new RoleClaimDTO
                  {
                      Id = rc.Id,
                      RoleId = rc.RoleId,
                      RoleName = r.Name,
                      ClaimType = rc.ClaimType,
                      ClaimValue = rc.ClaimValue
                  }).ToListAsync();
}


}
