using Microsoft.EntityFrameworkCore;

public class EnrollmentRepository
{
    private readonly SchoolContext _context;

    public EnrollmentRepository(SchoolContext context)
    {
        _context = context;
    }
   
}
