namespace CourseRegistrationSystem.Repositories;

public class StudentRepository
{
    private readonly SchoolContext _schoolContext;

    public StudentRepository(SchoolContext schoolContext)
    {
        _schoolContext = schoolContext;
    }

    public Student GetStudentById(int id)
    {
        return _schoolContext.Students.FirstOrDefault(o=> o.ID == id);
    }
}