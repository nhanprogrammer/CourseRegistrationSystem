using Microsoft.EntityFrameworkCore;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {
    }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Student> Students { get; set; }

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
              v => v.HasValue ? v.Value : default(DateTime),  // Chuyển đổi từ DateTime? thành DateTime
              v => (DateTime?)DateTime.SpecifyKind(v, DateTimeKind.Utc)  // Chuyển đổi ngược lại từ DateTime thành DateTime?
          );
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<Student>())
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                // Kiểm tra nếu EnrollmentDate có giá trị không phải null
                if (entry.Entity.EnrollmentDate.HasValue)
                {
                    entry.Entity.EnrollmentDate = DateTime.SpecifyKind(entry.Entity.EnrollmentDate.Value, DateTimeKind.Utc);
                }
            }
        }

        return base.SaveChanges();
    }
}