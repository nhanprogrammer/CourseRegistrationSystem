using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/")]
[ApiController()]
public class StudentController : ControllerBase
{
    private readonly StudentService _studentService;

    public StudentController(StudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpDelete("students")]
    public IActionResult RemoveStudent([FromQuery] int studentId)
    {
        var response = _studentService.RemoveStudent(studentId);

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
