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

}
