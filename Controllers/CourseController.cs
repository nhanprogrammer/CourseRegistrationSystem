using CourseSystem.Helpers;
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
    public IActionResult GetCourses()
    {
        try
        {
            var courses = _courseService.GetCourses();

            if (courses.Count() <= 0)
            {
                return NotFound(new { Status = 0, Description = "Không tìm thấy khóa học" });
            }

            return Ok(new { Status = 0, Description = courses });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Status = 1, Description = "Lỗi hệ thống." });
        }
    }

    // Tạo mới khóa học
    [HttpPost("courses")]
    public IActionResult CreateCourse([FromBody] Course newCourse)
    {
        try
        {
            _courseService.CreateNewCourse(newCourse.Title, newCourse.Credits ?? 0);
            return Ok(new { Status = 0, Description = "Tạo khóa học thành công." });
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new { Status = 1, Description = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Status = 1, Description = "Lỗi hệ thống." });
        }
    }

    // Cập nhật khóa học
    [HttpPut("courses")]
    public IActionResult UpdateCourse([FromBody] Course updatedCourse)
    {
        try
        {
            _courseService.UpdateCourse(updatedCourse.CourseID ?? -1, updatedCourse.Title, updatedCourse.Credits ?? 0);
            return Ok(new { Status = 0, Description = "Cập nhật khóa học thành công." });
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new { Status = 1, Description = ex.Message });
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { Status = 0, Description = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Status = 1, Description = "Lỗi hệ thống." });
        }
    }

    // Xóa khóa học
    [HttpDelete("courses/{courseId}")]
    public IActionResult DeleteCourse(int courseId)
    {
        try
        {
            _courseService.DeleteCourse(courseId);
            return Ok(new { Status = 0, Description = "Xóa khóa học thành công." });
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { Status = 0, Description = ex.Message });
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new { Status = 1, Description = ex.Message });
        }
        catch (Exception ex)
        {
            Console.Write(ex);
            return StatusCode(500, new { Status = 1, Description = "Lỗi hệ thống." });
        }
    }
}