namespace CourseRegistrationSystem.Dtos;

public class EnrollmentDto
{
    public int EnrollmentID { get; set; }

    public CourseDto Course { get; set; }

    public StudentDto Student { get; set; }
    
    public string? Grade { get; set; }
}