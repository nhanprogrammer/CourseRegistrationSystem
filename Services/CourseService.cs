public class CourseService
{
    private readonly CourseRepository _courseRepository;

    public CourseService(CourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public List<Course> GetCourses()
    {
        return _courseRepository.GetAllCourses();
    }

    public void CreateNewCourse(string title, int credits)
    {
        var newCourse = new Course
        {
            Title = title,
            Credits = credits
        };

        _courseRepository.AddCourse(newCourse);
    }
}