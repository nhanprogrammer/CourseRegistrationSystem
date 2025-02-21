using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PermissionRepository
{
    private readonly SchoolContext _context;

    public PermissionRepository(SchoolContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<RoleClaim>> GetAllPermissionsAsync()
    {
        return await _context.RoleClaims.ToListAsync();
    }
}