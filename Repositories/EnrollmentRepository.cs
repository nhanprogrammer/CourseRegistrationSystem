using CourseRegistrationSystem.Dtos;
using Microsoft.EntityFrameworkCore;

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
        return _schoolContext.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .FirstOrDefault(o => o.EnrollmentID == id);
    }

    public List<EnrollmentDto> GetAllEnrollments()
    {
        return _schoolContext.Enrollments.Select(enrollment => new EnrollmentDto()
        {
            EnrollmentID = enrollment.EnrollmentID,
            Grade = enrollment.Grade,
            Student = new StudentDto // Sử dụng StudentDto
            {
                ID = enrollment.Student.ID,
                LastName = enrollment.Student.LastName,
                FirstMidName = enrollment.Student.FirstMidName,
                EnrollmentDate = enrollment.Student.EnrollmentDate
            },
            Course = new CourseDto // Sử dụng CourseDto
            {
                CourseID = enrollment.Course.CourseID,
                Title = enrollment.Course.Title,
                Credits = enrollment.Course.Credits
            }
        }).ToList();
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