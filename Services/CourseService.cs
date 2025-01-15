using CourseRegistrationSystem.Repositories;
using CourseSystem.Helpers;

public class CourseService
{
    private readonly CourseRepository _courseRepository;
    private readonly EnrollmentRepository _enrollmentRepository;
    public CourseService(CourseRepository courseRepository, EnrollmentRepository enrollmentRepository)
    {
        _courseRepository = courseRepository;
        _enrollmentRepository = enrollmentRepository;
    }
    public Course? getCourseById(int courseId)
    {
        var course = _courseRepository.GetCourseById(courseId);
        return course;
    }

    // Lấy danh sách khóa học
    public List<Course> GetCourses()
    {
        return _courseRepository.GetAllCourses();
    }

    // Tạo mới khóa học
    public void CreateNewCourse(string title, int credits)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new BadRequestException("Tiêu đề khóa học không được để trống.");
        }

        if (credits <= 0 || credits > 10)
        {
            throw new BadRequestException("Số tín chỉ phải nằm trong khoảng từ 1 đến 10.");
        }

        var newCourse = new Course
        {
            Title = title,
            Credits = credits
        };

        _courseRepository.AddCourse(newCourse);
    }

    // Cập nhật thông tin khóa học
    public void UpdateCourse(int courseId, string title, int credits)
    {
        var course = _courseRepository.GetCourseById(courseId);

        if (course == null)
        {
            throw new NotFoundException("Không tìm thấy khóa học.");
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new BadRequestException("Tiêu đề khóa học không được để trống.");
        }

        if (credits <= 0 || credits > 10)
        {
            throw new BadRequestException("Số tín chỉ phải nằm trong khoảng từ 1 đến 10.");
        }

        course.Title = title;
        course.Credits = credits;

        _courseRepository.UpdateCourse(course);
    }

    // Xóa khóa học
    public void DeleteCourse(int courseId)
    {
        var course = _courseRepository.GetCourseById(courseId);

        if (course == null)
        {
            throw new NotFoundException("Không tìm thấy khóa học.");
        }
        if (_enrollmentRepository.HasEnrollmentsForCourse(courseId))
        {
            throw new BadRequestException("Không thể xóa khóa học vì có sinh viên đã đăng ký.");
        }
        _courseRepository.DeleteCourse(course);
    }
}
