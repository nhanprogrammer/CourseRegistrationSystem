using CourseRegistrationSystem.Dtos;
using CourseRegistrationSystem.Services;
using CourseSystem.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CourseRegistrationSystem.Controllers;

[Route("api/enrollment")]
[ApiController]
public class EnrollmentController : ControllerBase
{
    private readonly EnrollmentService _enrollmentService;

    public EnrollmentController(EnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetSingleEnrollment(int id)
    {
        try
        {
            var enrollment = _enrollmentService.GetEnrollmentById(id);

            return Ok(new { Status = 0, Description = enrollment });
        }
        catch (NotFoundException e)
        {
            return NotFound(new { Status = 0, Description = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { Status = 1, Description = "Lỗi hệ thống." });
        }
    }

    [HttpGet]
    public IActionResult GetAllEnrollment()
    {
        try
        {
            var enrollments = _enrollmentService.GetAllEnrollments();
            if (enrollments.Count == 0)
            {
                return NotFound(new { Status = 0, Description = "Không tìm thấy thông tin ghi danh khóa học." });
            }

            return Ok(new { Status = 0, Description = enrollments });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { Status = 1, Description = "Lỗi hệ thống." });
        }
    }

    [HttpPost]
    public IActionResult AddEnrollment([FromBody] EnrollmentCreateDto enrollment)
    {
        try
        {
            _enrollmentService.CreateEnrollment(enrollment);
            return Ok(new { Status = 0, Description = "Ghi danh khóa học thành công." });
        }
        catch (BadRequestException e)
        {
            return BadRequest(new { Status = 1, Description = e.Message });
        }
        catch (NotFoundException e)
        {
            return NotFound(new { Status = 0, Description = e.Message });
        }
        catch (ConflictException e)
        {
            return Conflict(new { Status = 1, Description = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { Status = 1, Description = "Lỗi hệ thống." });
        }
    }

    [HttpPut]
    public IActionResult UpdateEnrollment([FromBody] Enrollment enrollment)
    {
        try
        {
            _enrollmentService.UpdateEnrollment(enrollment);
            return Ok(new { Status = 0, Description = "Cập nhật grade thành công." });
        }
        catch (BadRequestException e)
        {
            return BadRequest(new { Status = 1, Description = e.Message });
        }
        catch (NotFoundException e)
        {
            return NotFound(new { Status = 0, Description = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { Status = 1, Description = "Lỗi hệ thống." });
        }
    }

    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult DeleteEnrollment(int id)
    {
        try
        {
            _enrollmentService.DeleteEnrollment(id);
            return Ok(new { Status = 0, Description = "Xóa thông tin ghi danh thành công." });
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
}