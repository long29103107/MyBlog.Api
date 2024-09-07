namespace Contracts.Domain.Exceptions.Abtractions;

public class ErrorException : DomainException
{
    public ErrorException(string message)
        : base("Error", message)
    {
    }
}
