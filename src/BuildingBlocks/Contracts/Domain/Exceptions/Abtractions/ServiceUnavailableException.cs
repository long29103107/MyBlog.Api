namespace Contracts.Domain.Exceptions.Abtractions;
public abstract class ServiceUnavailableException : DomainException
{
    protected ServiceUnavailableException(string message)
        : base("Service Unavailable", message)
    {
    }
}