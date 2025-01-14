using Microsoft.EntityFrameworkCore;

public class CourseRepository
{
    private readonly SchoolContext _context;

    public CourseRepository(SchoolContext context)
    {
        _context = context;
    }

    // Lấy thông tin khóa học theo ID
    public Course GetCourseById(int courseId)
    {
        var course = _context.Courses.FirstOrDefault(c => c.CourseID == courseId);
        if (course == null)
        {
            throw new KeyNotFoundException("Không tìm thấy khóa học với ID: " + courseId);
        }
        return course;
    }

    // Lấy danh sách tất cả các khóa học
    public List<Course> GetAllCourses()
    {
        return _context.Courses.ToList();
    }

    // Thêm mới khóa học
    public void AddCourse(Course course)
    {
        if (course == null)
        {
            throw new ArgumentNullException(nameof(course), "Course không được để trống.");
        }

        _context.Courses.Add(course);
        _context.SaveChanges();
    }

    // Cập nhật khóa học
    public void UpdateCourse(Course course)
    {
        if (course == null)
        {
            throw new ArgumentNullException(nameof(course), "Course không được để trống.");
        }

        var existingCourse = _context.Courses.FirstOrDefault(c => c.CourseID == course.CourseID);
        if (existingCourse == null)
        {
            throw new KeyNotFoundException("Không tìm thấy khóa học để cập nhật.");
        }

        existingCourse.Title = course.Title;
        existingCourse.Credits = course.Credits;

        _context.Courses.Update(existingCourse);
        _context.SaveChanges();
    }

    // Xóa khóa học
    public void DeleteCourse(Course course)
    {
        if (course == null)
        {
            throw new ArgumentNullException(nameof(course), "Course không được để trống.");
        }

        var existingCourse = _context.Courses.FirstOrDefault(c => c.CourseID == course.CourseID);
        if (existingCourse == null)
        {
            throw new KeyNotFoundException("Không tìm thấy khóa học để xóa.");
        }

        _context.Courses.Remove(existingCourse);
        _context.SaveChanges();
    }
}
