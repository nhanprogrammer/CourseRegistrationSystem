using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Enrollment
{
    [Key]
    public int? EnrollmentID { get; set; }
    [ForeignKey("Course")]
    public int? CourseID { get; set; }
    [ForeignKey("Student")]
    public int? StudentID { get; set; }
    public string? Grade { get; set; }

    //Mối quan hệ n-1: Một sinh viên chỉ đăng ký một lớp học
    public Course? Course { get; set; }

    //Mối quan hệ n-1: Một lớp học có nhiều sinh viên đăng ký
    public Student? Student { get; set; }
}
