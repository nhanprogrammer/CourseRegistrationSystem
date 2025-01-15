using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CourseRegistrationSystem.Repositories
{
    public class StudentRepository
    {
        private readonly SchoolContext _schoolContext;

        public StudentRepository(SchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
        }

        public Student GetStudentById(int studentId)
        {
            return _schoolContext.Students.FirstOrDefault(o => o.ID == studentId);
        }

        public Student GetSingleStudent(int studentId)
        {
            return _schoolContext.Students.FirstOrDefault(s => s.ID == studentId);
        }

        public List<Student> GetAllStudents()
        {
            return _schoolContext.Students.ToList();
        }

        public void AddStudent(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Sinh viên không tồn tại");
            }

            _schoolContext.Students.Add(student);
            _schoolContext.SaveChanges();
        }

        public void UpdateStudent(Student student)
        {
            _schoolContext.Students.Update(student);
            _schoolContext.SaveChanges();
        }

        public void DeleteStudent(int studentId)
        {
            var student = GetSingleStudent(studentId);
            if (student != null)
            {
                _schoolContext.Students.Remove(student);
                _schoolContext.SaveChanges();
            }
        }

        public void DetachStudent(Student student)
        {
            var entry = _schoolContext.Entry(student);
            if (entry.State == EntityState.Detached)
            {
                return;
            }

            entry.State = EntityState.Detached;
        }

    }
}
