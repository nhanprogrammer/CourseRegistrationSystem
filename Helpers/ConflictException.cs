namespace CourseSystem.Helpers;

public class ConflictException : Exception
{
    public ConflictException(string message) : base(message)
    {
        
    }
}