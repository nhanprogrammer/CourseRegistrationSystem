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

}
