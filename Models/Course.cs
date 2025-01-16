using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Course
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? CourseID { get; set; }

    // [Required(ErrorMessage = "Vui lòng nhập tiêu đề khóa học.")]
    // [StringLength(100, MinimumLength = 5, ErrorMessage = "Tiêu đề phải có độ dài từ 5 đến 100 ký tự.")]
    public string? Title { get; set; } = string.Empty;

    // [Required(ErrorMessage = "Vui lòng nhập số tín chỉ.")]
    // [Range(1, 10, ErrorMessage = "Số tín chỉ phải nằm trong khoảng từ 1 đến 10.")]
    public int? Credits { get; set; }

    // Mối quan hệ 1-n: Một lớp học có thể có nhiều sinh viên đăng ký
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
