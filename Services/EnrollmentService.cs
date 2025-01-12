public class EnrollmentService
{
    private readonly EnrollmentRepository _enrollmentRepository;

    public EnrollmentService(EnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }
}

