using CourseRegistrationSystem.Dtos;
using CourseRegistrationSystem.Repositories;
using CourseSystem.Helpers;

namespace CourseRegistrationSystem.Services;

public class EnrollmentService
{
    private readonly EnrollmentRepository _enrollmentRepository;
    private readonly CourseRepository _courseRepository;
    private readonly StudentRepository _studentRepository;


    public EnrollmentService(EnrollmentRepository enrollmentRepository, CourseRepository courseRepository,
        StudentRepository studentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _courseRepository = courseRepository;
        _studentRepository = studentRepository;
    }

    public EnrollmentDto GetEnrollmentById(int id)
    {
        Enrollment enrollment = _enrollmentRepository.GetEnrollmentById(id);

        if (enrollment == null)
        {
            throw new NotFoundException("Không tìm thấy thông tin ghi danh khóa học");
        }

        return new EnrollmentDto()
        {
            EnrollmentID = enrollment.EnrollmentID,
            Grade = enrollment.Grade,
            Student = new StudentDto()
            {
                ID = enrollment.Student.ID,
                LastName = enrollment.Student.LastName,
                FirstMidName = enrollment.Student.FirstMidName,
                EnrollmentDate = enrollment.Student.EnrollmentDate
            },
            Course = new CourseDto()
            {
                CourseID = enrollment.Course.CourseID,
                Title = enrollment.Course.Title,
                Credits = enrollment.Course.Credits
            },
        };
    }

    public List<EnrollmentDto> GetAllEnrollments()
    {
        return _enrollmentRepository.GetAllEnrollments();
    }

    public void CreateEnrollment(Enrollment enrollment)
    {
        if (enrollment == null)
        {
            throw new BadRequestException("Thông tin ghi danh khóa học không được rỗng.");
        }

        if (_courseRepository.GetSingleCourse(enrollment.CourseID) == null)
        {
            throw new NotFoundException("Khóa học không tồn tại.");
        }

        if (_studentRepository.GetStudentById(enrollment.StudentID) == null)
        {
            throw new NotFoundException("Sinh viên không tồn tại.");
        }

        if (_enrollmentRepository.isStudentRegistered(enrollment.StudentID, enrollment.CourseID))
        {
            throw new ConflictException("Sinh viên đã đăng ký khóa học này rồi.");
        }

        var enrollmentToAdd = new Enrollment()
        {
            StudentID = enrollment.StudentID,
            CourseID = enrollment.CourseID,
            Grade = ""
        };

        _enrollmentRepository.AddEnrollment(enrollmentToAdd);
    }

    public void UpdateEnrollment(Enrollment enrollment)
    {
        if (enrollment == null)
        {
            throw new BadRequestException("Thông tin ghi danh khóa học không được rỗng.");
        }

        Enrollment enrollmentExit = _enrollmentRepository.GetEnrollmentById(enrollment.EnrollmentID);

        if (enrollmentExit == null)
        {
            throw new NotFoundException("Thông tin ghi danh khóa học không tồn tại.");
        }

        enrollmentExit.Grade = enrollment.Grade ?? "";

        _enrollmentRepository.UpdateEnrollment(enrollmentExit);
    }

    public void DeleteEnrollment(int id)
    {
        var enrollment = _enrollmentRepository.GetEnrollmentById(id);
        if (enrollment == null)
        {
            throw new NotFoundException("Không tìm thấy thông tin ghi danh.");
        }

        _enrollmentRepository.RemoveEnrollment(enrollment);
    }
}