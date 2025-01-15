namespace CourseRegistrationSystem.Repositories;

public class EnrollmentRepository
{
    private readonly SchoolContext _schoolContext;

    public EnrollmentRepository(SchoolContext schoolContext)
    {
        _schoolContext = schoolContext;
    }

    public Enrollment GetEnrollmentById(int id)
    {
        return _schoolContext.Enrollments.FirstOrDefault(o => o.EnrollmentID == id);
    }

    public List<Enrollment> GetAllEnrollments()
    {
        return _schoolContext.Enrollments.ToList();
    }

    public void AddEnrollment(Enrollment enrollment)
    {
        _schoolContext.Enrollments.Add(enrollment);
        _schoolContext.SaveChanges();
    }
    
    public void UpdateEnrollment(Enrollment enrollment)
    {
        _schoolContext.Enrollments.Update(enrollment);
        _schoolContext.SaveChanges();
    }
    
    public bool isStudentRegistered(int studentId, int courseId)
    {
        Enrollment isRegistered = _schoolContext.Enrollments
                                        .FirstOrDefault(o => o.StudentID == studentId && o.CourseID == courseId);

        return isRegistered != null;
    }

    public bool HasEnrollmentsForStudent(int studentId)
    {
        return _schoolContext.Enrollments.Any(e => e.StudentID == studentId);
    }

    public bool HasEnrollmentsForCourse(int courseId)
    {
        return _schoolContext.Enrollments.Any(e => e.CourseID == courseId);
    }

    public void RemoveEnrollment(Enrollment enrollment)
    {
        _schoolContext.Enrollments.Remove(enrollment);
        _schoolContext.SaveChanges();
    }

}
