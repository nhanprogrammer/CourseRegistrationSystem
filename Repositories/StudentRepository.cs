using Microsoft.EntityFrameworkCore;

public class StudentRepository
{
    private readonly SchoolContext _context;
    private readonly EnrollmentRepository _enrollmentRepository;

    public StudentRepository(SchoolContext context,EnrollmentRepository enrollmentRepository)
    {
        _context = context;
        _enrollmentRepository = enrollmentRepository;
    }

    public Student GetStudentById(int studentId)
    {
        return _context.Students
                       .FirstOrDefault(s => s.ID == studentId);
    }
   public void RemoveStudent(Student student)
    {
        _context.Students.Remove(student);
        _context.SaveChanges();
    }
}
