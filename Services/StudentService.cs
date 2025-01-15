public class StudentService
{
    private readonly StudentRepository _studentRepository;
    private readonly EnrollmentRepository _enrollmentRepository;
    public StudentService(StudentRepository studentRepository, EnrollmentRepository enrollmentRepository)
    {
        _studentRepository = studentRepository;
        _enrollmentRepository = enrollmentRepository;
    }

    public ApiResponse<string> RemoveStudent(int studentId)
    {
        Console.WriteLine("Removing student with ID: " + studentId);
        var student = _studentRepository.GetStudentById(studentId);
        if (student == null)
        {
            return new ApiResponse<string>(1, $"Sinh viên với ID {studentId} không tồn tại.");
        }

        if (_enrollmentRepository.HasEnrollmentsForStudent(studentId))
        {
            return new ApiResponse<string>(1, $"Không thể xóa sinh viên vì có khóa học đã đăng ký.");
        }

        try
        {
            _studentRepository.RemoveStudent(student);
            return new ApiResponse<string>(0, "Xóa sinh viên thành công.");
        }
        catch (Exception ex)
        {
            return new ApiResponse<string>(1, ex.Message);
        }
    }
}

