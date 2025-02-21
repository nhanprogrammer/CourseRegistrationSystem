using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    private readonly UserRepository _userRepository;

    public UserController(UserService userService, UserRepository userRepository)
    {
        _userService = userService;
        _userRepository = userRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpGet("username/{username}")]
    public async Task<IActionResult> GetUserByUsername(string username)
    {
        var user = await _userService.GetUserByUsernameAsync(username);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        await _userService.AddUserAsync(user);
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        await _userService.UpdateUserAsync(user);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var (token, user, roles, claims) = await _userService.AuthenticateAsync(loginRequest.Username, loginRequest.Password);
        if (token == null)
        {
            return Unauthorized();
        }
        return Ok(new
        {
            StatusCode = 0,
            Token = token,
            Email = user.Email,
            Roles = roles,
            Claims = claims
        });
    }

    [HttpPost("register")]
   public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
{
    try
    {
        var user = new User
        {
            UserName = registerRequest.Username,
            Email = registerRequest.Email,
            PasswordHash = registerRequest.Password
        };

        await _userService.AddUserAsync(user);
        return Ok(new { Status = 0, Message = "Đăng ký thành công!" });
    }
    catch (Exception e)
    {
        Console.WriteLine(e + " Lỗi hệ thống");
        return StatusCode(500, new { Status = 1, Message = "Lỗi máy chủ." });
    }
}


    [HttpGet("all")]
    public async Task<IActionResult> GetAllUsersWithRolesAndPermissions()
    {
        var users = await _userService.GetAllUsersWithRolesAndPermissionsAsync();
        return Ok(users);
    }

    [HttpPut("updateClaims/{userId}")]
    public async Task<IActionResult> UpdateUserClaims(int userId, [FromBody] List<UserClaimDTO> claims)
    {
        bool success = await _userService.UpdateUserClaims(userId, claims);
        if (!success) return NotFound("User not found");

        return Ok("User claims updated successfully");
    }

    [HttpPut("updateRoles/{userId}")]
    public async Task<IActionResult> UpdateUserRoles(int userId, [FromBody] List<string> roleIds)
    {
        var isUpdated = await _userRepository.UpdateUserRolesAsync(userId, roleIds);
        if (!isUpdated)
            return NotFound("User not found");

        return Ok("User roles updated successfully!");
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        var isDeleted = await _userService.DeleteUserAsync(userId);
        if (!isDeleted)
        {
            return NotFound(new { message = "User không tồn tại." });
        }

        return Ok(new { message = "Xóa user thành công." });
    }

}