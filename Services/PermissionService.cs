using System.Collections.Generic;
using System.Threading.Tasks;

public class PermissionService
{
    private readonly PermissionRepository _permissionRepository;

    public PermissionService(PermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository ?? throw new ArgumentNullException(nameof(permissionRepository));
    }

    public async Task<List<RoleClaim>> GetAllPermissionsAsync()
    {
        return await _permissionRepository.GetAllPermissionsAsync();
    }
}