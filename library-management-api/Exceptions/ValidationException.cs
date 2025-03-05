namespace library_management_api.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message) { }
}