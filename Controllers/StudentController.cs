using CourseRegistrationSystem.Services;
using CourseSystem.Helpers;
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
            try
            {
                var response = _service.GetAll();
                if (response.Count == 0)
                {
                    return NotFound(new { Status = 0, Description = "Không tìm thấy sinh viên." });
                }

                return Ok(new { Status = 0, Description = response });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Status = 1, Description = "Lỗi hệ thống." });
            }
        }

        // [HttpGet("{id}")]
        // public IActionResult GetById(int id)
        // {
        //     var response = _service.GetById(id);
        //     return StatusCode(response.Status, response);
        // }

        [HttpPost]
        public IActionResult Create([FromBody] Student student)
        {
            try
            {
                _service.Add(student);
                return Ok(new { Status = 0, Message = "Thêm sinh viên thành công." });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { Status = 1, Message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Status = 0, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = 1, Message = "Lỗi hệ thống." });
            }
        }


        [HttpPut]
        public IActionResult Update([FromQuery] int id, [FromBody] Student student)
        {
            try
            {
                _service.Update(id, student);
                return Ok(new { Status = 0, Message = "Cập nhật sinh viên thành công." });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { Status = 1, Message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Status = 0, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = 1, Message = "Lỗi hệ thống." });
            }
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

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok(new { Status = 0, Message = "Xóa sinh viên thành công." });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Status = 0, Message = ex.Message });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { Status = 1, Message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new { Status = 1, Message = "Lỗi hệ thống." });
            }
        }
    }
}