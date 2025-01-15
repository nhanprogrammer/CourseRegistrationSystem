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

    public List<Course> GetCourses()
    {
        return _courseRepository.GetAllCourses();
    }
    public void CreateNewCourse(string title, int credits)
    {
        var newCourse = new Course
        {
            Title = title,
            Credits = credits
        };

        _courseRepository.AddCourse(newCourse);
    }

    public ApiResponse<string> RemoveCourse(int courseId)
    {
        var course = _courseRepository.GetCourseById(courseId);
        if (course == null)
        {
            return new ApiResponse<string>(1, $"Khóa học với ID {courseId} không tồn tại.");
        }

        if (_enrollmentRepository.HasEnrollmentsForCourse(courseId))
        {
            return new ApiResponse<string>(1, $"Không thể xóa khóa học vì có sinh viên đã đăng ký.");
        }

        try
        {
            _courseRepository.RemoveCourse(course);
            return new ApiResponse<string>(0, "Xóa khóa học thành công.");
        }
        catch (Exception ex)
        {
            return new ApiResponse<string>(1, ex.Message);
        }
    }
}
