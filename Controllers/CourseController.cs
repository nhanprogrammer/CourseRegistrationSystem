using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/")]
[ApiController()]
public class CourseController : ControllerBase
{
    private readonly CourseService _courseService;

    public CourseController(CourseService courseService)
    {
        _courseService = courseService;
    }
    [HttpGet("courses")]
    public ActionResult<ApiResponse<List<Course>>> GetCourses()
    {
        var courses = _courseService.GetCourses();
        if (courses == null || courses.Count == 0)
        {
            return NotFound(new ApiResponse<List<Course>>(404, "Không tìm thấy khóa học."));
        }

        return Ok(new ApiResponse<List<Course>>(200, courses));
    }
    [HttpPost("courses")]
    public IActionResult CreateCourse([FromBody] Course newCourse)
    {
        // if (!ModelState.IsValid)
        // {
        //    
        //     var errorMessages = ModelState.Values
        //         .SelectMany(v => v.Errors)
        //         .Select(e => e.ErrorMessage)
        //     return BadRequest(new ApiResponse<string>(400, "Dữ liệu không hợp lệ.\n" + errorMessages));
        // }
        if (newCourse == null)
        {
            return BadRequest("Không được bỏ trống khóa học.");
        }

        if (string.IsNullOrEmpty(newCourse.Title))
        {
            return BadRequest("Tiêu đề khóa học không được bỏ trống.");
        }

        _courseService.CreateNewCourse(newCourse.Title, newCourse.Credits ?? 0);
        return Ok(new ApiResponse<List<Course>>(0, "Tạo khóa học thành công."));
    }


}
