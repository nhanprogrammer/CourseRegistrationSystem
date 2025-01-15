using CourseRegistrationSystem.Repositories;
using System.Collections.Generic;

namespace CourseRegistrationSystem.Services
{
    public class StudentService
    {
        private readonly StudentRepository _repository;

        public StudentService(StudentRepository repository)
        {
            _repository = repository;
        }

        public ApiResponse<IEnumerable<Student>> GetAll()
        {
            try
            {
                var students = _repository.GetAllStudents();
                if (students == null || !students.Any())
                {
                    return new ApiResponse<IEnumerable<Student>>(1, "Không tìm thấy sinh viên nào");
                }
                return new ApiResponse<IEnumerable<Student>>(0, students);
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<Student>>(1, ex.Message);
            }
        }

        public ApiResponse<Student> GetById(int id)
        {
            try
            {
                var student = _repository.GetStudentById(id);
                if (student == null)
                {
                    return new ApiResponse<Student>(1, "Không tìm thấy sinh viên");
                }
                return new ApiResponse<Student>(0, student);
            }
            catch (Exception ex)
            {
                return new ApiResponse<Student>(1, ex.Message);
            }
        }

        public ApiResponse<string> Add(Student student)
        {
            try
            {
                if (student == null)
                {
                    return new ApiResponse<string>(1, "Dữ liệu không hợp lệ");
                }

                if (student.EnrollmentDate <= DateTime.Now)
                {
                    return new ApiResponse<string>(1, "Ngày nhập học phải lớn hơn ngày hiện tại");
                }

                _repository.AddStudent(student);
                return new ApiResponse<string>(0, "Thêm sinh viên thành công");
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>(1, ex.Message);
            }
        }

        public ApiResponse<string> Update(int id, Student student)
        {
            try
            {
                if (id != student.ID)
                {
                    return new ApiResponse<string>(1, "ID không khớp");
                }

                if (student == null)
                {
                    return new ApiResponse<string>(1, "Dữ liệu không hợp lệ");
                }

                if (student.EnrollmentDate <= DateTime.Now)
                {
                    return new ApiResponse<string>(1, "Ngày nhập học phải lớn hơn ngày hiện tại");
                }

                var existingStudent = _repository.GetStudentById(id);
                if (existingStudent == null)
                {
                    return new ApiResponse<string>(1, "Không tìm thấy sinh viên");
                }
                _repository.DetachStudent(existingStudent);
                _repository.UpdateStudent(student);
                return new ApiResponse<string>(0, "Cập nhật sinh viên thành công");
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>(1, ex.Message);
            }
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

        public ApiResponse<string> Delete(int id)
        {
            try
            {
                var student = _repository.GetStudentById(id);
                if (student == null)
                {
                    return new ApiResponse<string>(1, "Không tìm thấy sinh viên");
                }

                _repository.DeleteStudent(id);
                return new ApiResponse<string>(0, "Xóa sinh viên thành công");
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>(1, ex.Message);
            }
        }
    }
}
