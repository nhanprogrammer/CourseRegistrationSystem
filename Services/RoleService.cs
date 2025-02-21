using System.Collections.Generic;
using System.Threading.Tasks;

public class RoleService
{
    private readonly RoleRepository _roleRepository;

    public RoleService(RoleRepository roleRepository)
    {
        _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
    }

    public async Task<List<RoleDTO>> GetAllRolesAsync()
    {
        return await _roleRepository.GetAllRolesAsync();
    }
}