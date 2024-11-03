﻿namespace Contracts.Domain.Exceptions.Abtractions;

public class BadRequestException : DomainException
{
    public BadRequestException(string message)
        : base("Bad Request", message)
    {
    }
}
