using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

public class UserService
{
    private readonly UserRepository _userRepository;
private readonly SchoolContext _context;
    private readonly RoleRepository _roleRepository;
    private readonly IConfiguration _configuration;
    public UserService(UserRepository userRepository, RoleRepository roleRepository, SchoolContext schoolContext, IConfiguration configuration)
    {
        roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _context = schoolContext ?? throw new ArgumentNullException(nameof(schoolContext));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }


    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await _userRepository.GetUserByIdAsync(userId);
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _userRepository.GetUserByUsernameAsync(username);
    }

    public async Task AddUserAsync(User user)
    {
        Console.WriteLine(user.Email + " UserEmail");
        Console.WriteLine(user.UserName + " UserName");
        Console.WriteLine(user.PasswordHash + " PasswordHash");
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User cannot be null");
        }

        if (string.IsNullOrEmpty(user.PasswordHash))
        {
            throw new ArgumentException("Password cannot be null or empty", nameof(user.PasswordHash));
        }

        user.PasswordHash = HashPassword(user.PasswordHash);
        Console.WriteLine(user.PasswordHash + " PasswordHash after hashing");
        await _userRepository.AddUserAsync(user);
    }


    public async Task UpdateUserAsync(User user)
    {
        await _userRepository.UpdateUserAsync(user);
    }

    public async Task DeleteUserAsync(int userId)
    {
        await _userRepository.DeleteUserAsync(userId);
    }

    public async Task<(string token, User user, List<string> roles, List<string> claims)> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null || !VerifyPassword(password, user.PasswordHash))
        {
            return (null, null, null, null);
        }

        var roles = await _userRepository.GetUserRolesAsync(user.Id);
        var claims = await _userRepository.GetUserClaimsAsync(user.Id);

        var tokenClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Id.ToString())
        };

        foreach (var role in roles)
        {
            tokenClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        foreach (var claim in claims)
        {
            tokenClaims.Add(new Claim("Permission", claim));
        }

        // Tạo JWT token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(tokenClaims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return (tokenHandler.WriteToken(token), user, roles, claims);
    }


    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private bool VerifyPassword(string password, string storedHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, storedHash);
    }

    public async Task<List<UserWithRolesAndPermissions>> GetAllUsersWithRolesAndPermissionsAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        var result = new List<UserWithRolesAndPermissions>();

        foreach (var user in users)
        {
            var roles = await _userRepository.GetAllUserRolesAsync(user.Id);

            if (roles.Any(role => role.Name == "CAP1"))
            {
                continue;
            }

            var permissions = await _userRepository.GetAllUserClaimsAsync(user.Id);

            result.Add(new UserWithRolesAndPermissions
            {
                Id = user.Id,
                FullName = user.UserName,
                Roles = roles,
                Claims = permissions
            });
        }
        return result;
    }


    public async Task<bool> UpdateUserClaims(int userId, List<UserClaimDTO> claims)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) return false;

        var newClaims = claims.ConvertAll(c => new UserClaim
        {
            UserId = userId,
            ClaimType = c.ClaimType,
            ClaimValue = c.ClaimValue
        });

        await _userRepository.UpdateUserClaimsAsync(userId, newClaims);
        return true;
    }

    public async Task<bool> DeleteUserAsync(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return false; 
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true; 
    }


}
