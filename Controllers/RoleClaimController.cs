using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class RoleClaimController : ControllerBase
{
    private readonly RoleClaimRepository _roleClaimRepository;

    public RoleClaimController(RoleClaimRepository roleClaimRepository)
    {
        _roleClaimRepository = roleClaimRepository;
    }

    [HttpGet("claims")]
public async Task<IActionResult> GetRoleClaimsByRoleIds([FromQuery] string roleIds)
{
    if (string.IsNullOrEmpty(roleIds))
    {
        return BadRequest(new { message = "RoleIds are required" });
    }

    List<int> roleIdList;
    try
    {
        roleIdList = roleIds.Split(',')
                            .Select(int.Parse)
                            .ToList();
    }
    catch
    {
        return BadRequest(new { message = "Invalid RoleIds format" });
    }

    var roleClaims = await _roleClaimRepository.GetAllUsersWithRolesAndPermissionsAsync(roleIdList);

    if (roleClaims == null || !roleClaims.Any())
    {
        return NotFound(new { message = "No RoleClaims found for these RoleIds" });
    }

    return Ok(roleClaims);
}

}
