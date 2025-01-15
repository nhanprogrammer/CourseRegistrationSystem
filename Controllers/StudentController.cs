using CourseRegistrationSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseRegistrationSystem.Controllers
{
    [ApiController]
    [Route("api/student")]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _service;

        public StudentController(StudentService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _service.GetAll();
            return StatusCode(response.Status, response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var response = _service.GetById(id);
            return StatusCode(response.Status, response);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Student student)
        {
            var response = _service.Add(student);
            return StatusCode(response.Status, response);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Student student)
        {
            var response = _service.Update(id, student);
            return StatusCode(response.Status, response);
        }

        // [HttpPut("{id}")]
        // public IActionResult Update(int id, [FromBody] Student student)
        // {
        //     if (!ModelState.IsValid) return BadRequest();

        //     // Kiểm tra nếu sinh viên tồn tại
        //     var existingStudent = _service.GetById(id);
        //     if (existingStudent == null)
        //         return NotFound();

        //     // Cập nhật thông tin sinh viên
        //     existingStudent.LastName = student.LastName;
        //     existingStudent.FirstMidName = student.FirstMidName;
        //     existingStudent.EnrollmentDate = student.EnrollmentDate;

        //     _service.Update(existingStudent);
        //     return NoContent(); // Trả về 204 khi thành công
        // }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = _service.Delete(id);
            return StatusCode(response.Status, response);
        }
    }
}
