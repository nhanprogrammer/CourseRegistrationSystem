using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RoleRepository
{
    private readonly SchoolContext _context;

    public RoleRepository(SchoolContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Role> FindByIdAsync(int roleId)
    {
        return await _context.Roles.FindAsync(roleId);
    }

    public async Task<List<RoleDTO>> GetAllRolesAsync()
    {
        var roles = await _context.Roles
            .Include(r => r.RoleClaims)
            .Where(r => r.Name != "CAP1")
            .ToListAsync();

        return roles.Select(r => new RoleDTO
        {
            Id = r.Id,
            Name = r.Name,
            Claims = r.RoleClaims.Select(rc => rc.ClaimValue).ToList()
        }).ToList();
    }

}
