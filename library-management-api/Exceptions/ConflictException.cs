namespace library_management_api.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(string message) : base(message) { }
}