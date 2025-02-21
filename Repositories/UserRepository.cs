using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserRepository
{
    private readonly SchoolContext _context;

    public UserRepository(SchoolContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }


    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }


    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);
    }

    public async Task AddUserAsync(User user)
    {
        Console.WriteLine(user.Email + " UserUser");
        Console.WriteLine(user.UserName + " UserName");
        Console.WriteLine(user.PasswordHash + " PasswordHash");
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User object cannot be null");
        }

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<List<string>> GetUserRolesAsync(int userId)
    {
        return await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.Role.Name)
            .ToListAsync();
    }
    public async Task<List<RoleDTO>> GetAllUserRolesAsync(int userId)
    {
        return await (from ur in _context.UserRoles
                      join r in _context.Roles on ur.RoleId equals r.Id
                      where ur.UserId == userId
                      select new RoleDTO
                      {
                          Id = r.Id,  // Thêm RoleId
                          Name = r.Name // RoleName
                      }).ToListAsync();
    }

    public async Task<List<string>> GetUserClaimsAsync(int userId)
    {
        return await _context.UserClaims
            .Where(uc => uc.UserId == userId)
            .Select(uc => uc.ClaimValue)
            .ToListAsync();
    }

    public async Task<List<UserClaimDTO>> GetAllUserClaimsAsync(int userId)
    {
        return await _context.UserClaims
            .Where(uc => uc.UserId == userId)
            .Select(uc => new UserClaimDTO
            {
                ClaimType = uc.ClaimType,
                ClaimValue = uc.ClaimValue
            })
            .ToListAsync();
    }

    public async Task UpdateUserClaimsAsync(int userId, List<UserClaim> newClaims)
    {
        var existingClaims = await _context.UserClaims
            .Where(uc => uc.UserId == userId)
            .ToListAsync();

        // Xóa claims cũ
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                _context.UserClaims.RemoveRange(existingClaims);
                await _context.SaveChangesAsync();

                await _context.UserClaims.AddRangeAsync(newClaims);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Transaction failed: {ex.Message}");
                await transaction.RollbackAsync();
            }
        }

    }
    public async Task<bool> UpdateUserRolesAsync(int userId, List<string> roleIds)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            return false;

        // Xóa tất cả roles cũ của user trong bảng trung gian
        var userRoles = _context.UserRoles.Where(ur => ur.UserId == userId);
        _context.UserRoles.RemoveRange(userRoles);
        await _context.SaveChangesAsync();

        foreach (var roleId in roleIds)
        {
            if (int.TryParse(roleId, out int roleIdInt))
            {
                _context.UserRoles.Add(new UserRole { UserId = userId, RoleId = roleIdInt });
            }
            else
            {
                // Log hoặc xử lý lỗi nếu roleId không thể chuyển đổi thành int
                return false;
            }
        }


        await _context.SaveChangesAsync();
        return true;
    }
}
