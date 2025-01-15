public class EnrollmentService
{
    private readonly EnrollmentRepository _enrollmentRepository;

    public EnrollmentService(EnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public ApiResponse<string> RemoveEnrollment(int enrollmentId)
    {
        var enrollment = _enrollmentRepository.GetEnrollmentById(enrollmentId);
        if (enrollment == null)
        {
            return new ApiResponse<string>(1, $"Đăng ký với ID {enrollmentId} không tồn tại.");
        }

        try
        {
            _enrollmentRepository.RemoveEnrollment(enrollment);
            return new ApiResponse<string>(0, "Xóa đăng ký thành công.");
        }
        catch (Exception ex)
        {
            return new ApiResponse<string>(1, ex.Message);
        }
    }
}