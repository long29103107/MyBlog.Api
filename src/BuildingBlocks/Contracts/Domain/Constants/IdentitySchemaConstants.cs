﻿using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Contracts.Domain.Constants;

public static class IdentitySchemaConstants
{
    //public const string IdentitySchema = "Identity";
    public class Table
    {
        public const string Users = "Users";
        public const string Roles = "Roles";
        public const string UserRoles = "UserRoles";
        public const string UserLogins = "UserLogins";
        public const string UserTokens = "UserTokens";
    }
}