using CourseRegistrationSystem.Repositories;
using CourseSystem.Helpers;
using System.Collections.Generic;

namespace CourseRegistrationSystem.Services
{
    public class StudentService
    {
        private readonly StudentRepository _repository;
        private readonly EnrollmentRepository _enrollmentRepository;

        public StudentService(StudentRepository repository)
        {
            _repository = repository;
        }

        public List<Student> GetAll()
        {
            return _repository.GetAllStudents();
        }

        // public ApiResponse<Student> GetById(int id)
        // {
        //     try
        //     {
        //         var student = _repository.GetStudentById(id);
        //         if (student == null)
        //         {
        //             return new ApiResponse<Student>(0, "Không tìm thấy sinh viên");
        //         }
        //         return new ApiResponse<Student>(0, student);
        //     }
        //     catch (Exception ex)
        //     {
        //         return new ApiResponse<Student>(1, ex.Message);
        //     }
        // }

        public void Add(Student student)
        {
            if (student == null)
            {
                throw new BadRequestException("Thông tin sinh viên không được rỗng.");
            }

            if (student.EnrollmentDate <= DateTime.Now)
            {
                throw new BadRequestException("Ngày nhập học phải lớn hơn ngày hiện tại.");
            }

            _repository.AddStudent(student);
        }


        public void Update(int id, Student student)
        {
            if (student == null)
            {
                throw new BadRequestException("Dữ liệu không được rỗng.");
            }

            if (id != student.ID)
            {
                throw new BadRequestException("ID không khớp.");
            }

            if (student.EnrollmentDate <= DateTime.Now)
            {
                throw new BadRequestException("Ngày nhập học phải lớn hơn ngày hiện tại.");
            }

            var existingStudent = _repository.GetStudentById(id);
            if (existingStudent == null)
            {
                throw new NotFoundException("Không tìm thấy sinh viên.");
            }

            _repository.DetachStudent(existingStudent);
            _repository.UpdateStudent(student);

            // try
            // {
            //     // Tải đối tượng từ DB theo ID
            //     var existingStudent = _repository.GetStudentById(student.ID);

            //     // Nếu không tìm thấy sinh viên, trả về lỗi
            //     if (existingStudent == null)
            //     {
            //         return new ApiResponse<string>(404, "Không tìm thấy sinh viên");
            //     }

            //     // Cập nhật các giá trị cần thiết
            //     existingStudent.LastName = student.LastName;
            //     existingStudent.FirstMidName = student.FirstMidName;
            //     existingStudent.EnrollmentDate = student.EnrollmentDate;

            //     // Cập nhật đối tượng trong DbContext
            //     _repository.UpdateStudent(existingStudent);
            //     return new ApiResponse<string>(0, "Cập nhật sinh viên thành công");
            // }
            // catch (Exception ex)
            // {
            //     return new ApiResponse<string>(500, ex.Message);
            // }
        }

        public void Delete(int id)
        {
            var student = _repository.GetStudentById(id);
            if (student == null)
            {
                throw new NotFoundException("Không tìm thấy sinh viên.");
            }
            if (_enrollmentRepository.HasEnrollmentsForStudent(id))
            {
                throw new BadRequestException("Không thể xóa sinh viên vì có khóa học đã đăng ký.");
            }

            _repository.DeleteStudent(id);
        }

    }
}
