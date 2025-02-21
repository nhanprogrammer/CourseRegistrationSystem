using Microsoft.EntityFrameworkCore;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {
      
    }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserClaim> UserClaims { get; set; }
    public DbSet<UserLogin> UserLogins { get; set; }
    public DbSet<RoleClaim> RoleClaims { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course) // Mối quan hệ n-1 với Course
            .WithMany(c => c.Enrollments) // Một Course có nhiều Enrollment
            .HasForeignKey(e => e.CourseID)
            .OnDelete(DeleteBehavior.Restrict); // Ngăn xóa Course nếu có Enrollment

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Student) // Mối quan hệ n-1 với Student
            .WithMany(s => s.Enrollments) // Một Student có nhiều Enrollment
            .HasForeignKey(e => e.StudentID)
            .OnDelete(DeleteBehavior.Restrict); // Ngăn xóa Student nếu có Enrollment

        modelBuilder.Entity<Student>()
            .Property(s => s.EnrollmentDate)
            .HasConversion(
                v => v,
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );
            modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

            modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);

        modelBuilder.Entity<UserClaim>()
            .HasOne(uc => uc.User)
            .WithMany(u => u.UserClaims)
            .HasForeignKey(uc => uc.UserId);

        modelBuilder.Entity<UserLogin>()
            .HasKey(ul => new { ul.UserId, ul.LoginProvider });

        modelBuilder.Entity<UserLogin>()
            .HasOne(ul => ul.User)
            .WithMany(u => u.UserLogins)
            .HasForeignKey(ul => ul.UserId);

        modelBuilder.Entity<RoleClaim>()
            .HasOne(rc => rc.Role)
            .WithMany(r => r.RoleClaims)
            .HasForeignKey(rc => rc.RoleId);
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<Student>())
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                entry.Entity.EnrollmentDate = DateTime.SpecifyKind(entry.Entity.EnrollmentDate, DateTimeKind.Utc);
            }
        }

        return base.SaveChanges();
    }
}
