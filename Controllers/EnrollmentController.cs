using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/")]
[ApiController()]
public class EnrollmentController : ControllerBase
{
    private readonly EnrollmentService _enrollmentService;

    public EnrollmentController(EnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [HttpDelete("enrollments")]
    public IActionResult DeleteEnrollment([FromQuery] int enrollmentid)
    {
        if (enrollmentid <= 0)
        {
            return BadRequest(new ApiResponse<string>(1, "ID không hợp lệ."));
        }

        var response = _enrollmentService.RemoveEnrollment(enrollmentid);
        if (response.ErrorCode == 0)
        {
            return Ok(response);
        }
        else if (response.ErrorCode == 1)
        {
            if (response.Description.Contains("không tồn tại"))
            {
                return NotFound(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        else
        {
            return StatusCode(1, response);
        }
    }
}
