

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Student
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int ID { get; set; }
	// [Required(ErrorMessage = "Họ không được để trống.")]
    // [StringLength(50, MinimumLength = 2, ErrorMessage = "Họ phải từ 2 đến 50 ký tự.")]
    // [Display(Name = "Họ")]
    // [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "Họ phải bắt đầu bằng chữ cái viết hoa.")]
    public string? LastName { get; set; }

    // [Required(ErrorMessage = "Tên không được để trống.")]
    // [StringLength(50, MinimumLength = 2, ErrorMessage = "Tên phải từ 2 đến 50 ký tự.")]
    // [Display(Name = "Tên")]
    // [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "Tên phải bắt đầu bằng chữ cái viết hoa.")]
    public string? FirstMidName { get; set; }

    // [Display(Name = "Ngày nhập học")]
    // [DataType(DataType.Date)]
    // [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime EnrollmentDate { get; set; }

	//Mối quan hệ 1-n: Một sinh viên có thể đăng ký nhiều lớp học
	public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
