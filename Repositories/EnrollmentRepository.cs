using Microsoft.EntityFrameworkCore;

public class EnrollmentRepository
{
    private readonly SchoolContext _context;

    public EnrollmentRepository(SchoolContext context)
    {
        _context = context;
    }

    public Enrollment? GetEnrollmentById(int enrollmentId)
    {
        return _context.Enrollments.FirstOrDefault(e => e.EnrollmentID == enrollmentId);
    }
    public bool HasEnrollmentsForCourse(int courseId)
    {
        return _context.Enrollments.Any(e => e.CourseID == courseId);
    }

    public bool HasEnrollmentsForStudent(int studentId)
    {
        return _context.Enrollments.Any(e => e.StudentID == studentId);
    }

    public bool CourseExists(int courseId)
    {
        return _context.Courses.Any(c => c.CourseID == courseId);
    }

    public bool StudentExists(int studentId)
    {
        return _context.Students.Any(s => s.ID == studentId);
    }

    public void RemoveEnrollment(Enrollment enrollment)
    {
        _context.Enrollments.Remove(enrollment);
        _context.SaveChanges();
    }
}
