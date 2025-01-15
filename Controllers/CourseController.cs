using Microsoft.AspNetCore.Mvc;

[Route("api/")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly CourseService _courseService;

    public CourseController(CourseService courseService)
    {
        _courseService = courseService;
    }

    // Lấy danh sách khóa học
    [HttpGet("courses")]
    public ActionResult<ApiResponse<List<Course>>> GetCourses()
    {
        var courses = _courseService.GetCourses();
        if (courses == null || courses.Count == 0)
        {
            return NotFound(new ApiResponse<List<Course>>(0, "Không tìm thấy khóa học."));
        }

        return Ok(new ApiResponse<List<Course>>(0, courses));
    }

    // Tạo mới khóa học
    [HttpPost("courses")]
    public IActionResult CreateCourse([FromBody] Course newCourse)
    {
        try
        {
            _courseService.CreateNewCourse(newCourse.Title, newCourse.Credits ?? 0);
            return Ok(new ApiResponse<string>(0, "Tạo khóa học thành công."));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiResponse<string>(1, ex.Message));
        }
    }

    // Cập nhật khóa học
    [HttpPut("courses/{courseId}")]
    public IActionResult UpdateCourse(int courseId, [FromBody] Course updatedCourse)
    {
        try
        {
            _courseService.UpdateCourse(courseId, updatedCourse.Title, updatedCourse.Credits ?? 0);
            return Ok(new ApiResponse<string>(0, "Cập nhật khóa học thành công."));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiResponse<string>(1, ex.Message));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse<string>(1, ex.Message));
        }
    }

    // Xóa khóa học
    [HttpDelete("courses/{courseId}")]
    public IActionResult DeleteCourse(int courseId)
    {
        try
        {
            _courseService.DeleteCourse(courseId);
            return Ok(new ApiResponse<string>(0, "Xóa khóa học thành công."));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse<string>(1, ex.Message));
        }
    }
}
