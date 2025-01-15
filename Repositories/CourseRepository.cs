using Microsoft.EntityFrameworkCore;

public class CourseRepository
{
    private readonly SchoolContext _context;
    private readonly EnrollmentRepository _enrollmentRepository;
    public CourseRepository(SchoolContext context, EnrollmentRepository enrollmentRepository)
    {
        _context = context;
        _enrollmentRepository = enrollmentRepository;
    }
    public Course GetCourseById(int courseId)
    {
        return _context.Courses
                       .FirstOrDefault(c => c.CourseID == courseId);
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

     public void RemoveCourse(Course course)
    {
        _context.Courses.Remove(course);
        _context.SaveChanges();
    }

}
