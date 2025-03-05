namespace library_management_api.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message){}
}