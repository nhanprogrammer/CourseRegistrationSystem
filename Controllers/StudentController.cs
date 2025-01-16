using CourseRegistrationSystem.Dtos;
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
                if (response == null || response.Count == 0)
                {
                    return StatusCode(404, new { Status = 0, Description = "Không tìm thấy sinh viên." });
                }
                return Ok(new { Status = 0, Description = response });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Status = 1, Description = "Lỗi hệ thống." });
            }
        }


        [HttpPost]
        public IActionResult Create([FromBody] StudentsDTO student)
        {
            try
            {
                _service.Add(student);
                return Ok(new { Status = 0, Description = "Thêm sinh viên thành công." });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { Status = 1, Description = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Status = 0, Description = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = 1, Description = "Lỗi hệ thống." });
            }
        }


        [HttpPut]
        public IActionResult Update([FromBody] StudentsDTO student)
        {
            try
            {
                _service.Update(student);
                return Ok(new { Status = 0, Description = "Cập nhật sinh viên thành công." });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { Status = 1, Description = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Status = 0, Description = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = 1, Description = "Lỗi hệ thống."});
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok(new { Status = 0, Description = "Xóa sinh viên thành công." });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Status = 0, Description = ex.Message });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { Status = 1, Description = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = 1, Description = "Lỗi hệ thống."});
            }
        }

    }
}
