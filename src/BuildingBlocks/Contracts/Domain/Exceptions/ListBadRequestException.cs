using Contracts.Domain.Exceptions.Abtractions;

namespace Contracts.Domain.Exceptions;

public sealed class ListBadRequestException : BadRequestException
{
    public ListBadRequestException(string message) : base(message)
    {
    }
}