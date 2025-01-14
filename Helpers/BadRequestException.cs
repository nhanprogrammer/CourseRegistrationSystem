namespace CourseSystem.Helpers;

public class BadRequestException : Exception
{
    public BadRequestException(string message):base(message){}
}