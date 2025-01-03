﻿
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Repository.Abstractions;

namespace MyBlog.Identity.Repository.Implements;

public class PermissionRepository : RepositoryIdentityBase<Permission, MyIdentityDbContext>, IPermissionRepository
{
    public PermissionRepository(MyIdentityDbContext context) : base(context)
    {
    }
}

