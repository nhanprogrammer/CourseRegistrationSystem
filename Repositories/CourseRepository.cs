using Microsoft.EntityFrameworkCore;

public class CourseRepository
{
    private readonly SchoolContext _context;

    public CourseRepository(SchoolContext context)
    {
        _context = context;
    }
    public Course GetCourseById(int courseId)
    {
        return _context.Courses
                       .FromSqlRaw("SELECT * FROM Courses WHERE CourseID = {0}", courseId)
                       .FirstOrDefault() ?? new Course();
    }
    
    public Course GetSingleCourse(int courseId)
    {
        return _context.Courses.FirstOrDefault(o=>o.CourseID == courseId);
    }

    public List<Course> GetAllCourses()
    {
        return _context.Courses.ToList();
    }

     public void AddCourse(Course course)
    {
        if (course == null)
        {
            throw new ArgumentNullException(nameof(course), "Course cannot be null.");
        }

        _context.Courses.Add(course);
        _context.SaveChanges();
    }
}
