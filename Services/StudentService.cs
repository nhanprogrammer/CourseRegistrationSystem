using CourseRegistrationSystem.Repositories;
using CourseSystem.Helpers;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using CourseRegistrationSystem.Dtos;

namespace CourseRegistrationSystem.Services
{
    public class StudentService
    {
        private readonly StudentRepository _repository;
        private readonly EnrollmentRepository _enrollmentRepository;

        public StudentService(StudentRepository repository, EnrollmentRepository enrollmentRepository)
        {
            _repository = repository;
            _enrollmentRepository = enrollmentRepository;
        }

        public List<Student> GetAll()
        {
            return _repository.GetAllStudents();
        }

        public void Add(StudentsDTO studentDto)
        {
            if (studentDto == null)
            {
                throw new BadRequestException("Thông tin sinh viên không được rỗng.");
            }


            if (string.IsNullOrWhiteSpace(studentDto.LastName))
            {
                throw new BadRequestException("Họ không được để trống.");
            }
            if (string.IsNullOrWhiteSpace(studentDto.FirstMidName))
            {
                throw new BadRequestException("Tên không được để trống.");
            }


            var vietnameseNameRegex = @"^[A-Za-zÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơưĂẮẰẲẴẶÂẤẦẨẪẬÊẾỀỂỄỆÔỐỒỔỖỘƠỚỜỞỠỢƯỨỪỬỮỰýỳỵỷỹÝỲỴỶỸ\s]+$";
            if (!Regex.IsMatch(studentDto.LastName, vietnameseNameRegex))
            {
                throw new BadRequestException("Họ phải là ký tự hợp lệ theo định dạng tiếng Việt.");
            }
            if (!Regex.IsMatch(studentDto.FirstMidName, vietnameseNameRegex))
            {
                throw new BadRequestException("Tên phải là ký tự hợp lệ theo định dạng tiếng Việt.");
            }


            DateTime? parsedEnrollmentDate = null;
            if (!string.IsNullOrWhiteSpace(studentDto.EnrollmentDate))
            {
                var dateFormat = "yyyy-MM-dd";
                if (!DateTime.TryParseExact(studentDto.EnrollmentDate, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tempDate))
                {
                    throw new BadRequestException("Ngày nhập học phải có định dạng 'yyyy-MM-dd'.");
                }

                if (tempDate <= DateTime.Now.Date)
                {
                    throw new BadRequestException("Ngày nhập học phải lớn hơn ngày hiện tại.");
                }

                parsedEnrollmentDate = tempDate;
            }


            var student = new Student
            {
                LastName = studentDto.LastName,
                FirstMidName = studentDto.FirstMidName,
                EnrollmentDate = parsedEnrollmentDate
            };


            _repository.AddStudent(student);
        }


        public void Update(StudentsDTO studentDto)
        {
            if (studentDto == null)
            {
                throw new BadRequestException("Thông tin sinh viên không được rỗng.");
            }


            if (string.IsNullOrWhiteSpace(studentDto.LastName))
            {
                throw new BadRequestException("Họ không được để trống.");
            }
            if (string.IsNullOrWhiteSpace(studentDto.FirstMidName))
            {
                throw new BadRequestException("Tên không được để trống.");
            }


            var vietnameseNameRegex = @"^[A-Za-zÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơưĂẮẰẲẴẶÂẤẦẨẪẬÊẾỀỂỄỆÔỐỒỔỖỘƠỚỜỞỠỢƯỨỪỬỮỰýỳỵỷỹÝỲỴỶỸ\s]+$";
            if (!Regex.IsMatch(studentDto.LastName, vietnameseNameRegex))
            {
                throw new BadRequestException("Họ phải là ký tự hợp lệ theo định dạng tiếng Việt.");
            }
            if (!Regex.IsMatch(studentDto.FirstMidName, vietnameseNameRegex))
            {
                throw new BadRequestException("Tên phải là ký tự hợp lệ theo định dạng tiếng Việt.");
            }


            DateTime? parsedEnrollmentDate = null;
            if (!string.IsNullOrWhiteSpace(studentDto.EnrollmentDate))
            {
                var dateFormat = "yyyy-MM-dd";
                if (!DateTime.TryParseExact(studentDto.EnrollmentDate, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tempDate))
                {
                    throw new BadRequestException("Ngày nhập học phải có định dạng 'yyyy-MM-dd'.");
                }

                if (tempDate <= DateTime.Now.Date)
                {
                    throw new BadRequestException("Ngày nhập học phải lớn hơn ngày hiện tại.");
                }

                parsedEnrollmentDate = tempDate;
            }

            var existingStudent = studentDto.ID.HasValue ? _repository.GetStudentById(studentDto.ID.Value) : null;

            if (existingStudent == null)
            {
                throw new NotFoundException("Không tìm thấy sinh viên.");
            }


            var student = new Student
            {
                ID = studentDto.ID,
                LastName = studentDto.LastName,
                FirstMidName = studentDto.FirstMidName,
                EnrollmentDate = parsedEnrollmentDate
            };


            _repository.DetachStudent(existingStudent);
            _repository.UpdateStudent(student);
        }


        public void Delete(int id)
        {
            var student = _repository.GetStudentById(id);
            if (student == null)
            {
                throw new KeyNotFoundException("Không tìm thấy sinh viên.");
            }
            if (_enrollmentRepository.HasEnrollmentsForStudent(id))
            {
                throw new BadRequestException("Không thể xóa sinh viên vì có khóa học đã đăng ký.");
            }
            _repository.DeleteStudent(id);
        }

    }
}
