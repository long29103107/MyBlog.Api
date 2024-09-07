namespace Contracts.Domain.Exceptions.Abtractions;

public abstract class ConflictException : DomainException
{
    protected ConflictException(string message)
        : base("Conflict", message)
    {
    }
}
