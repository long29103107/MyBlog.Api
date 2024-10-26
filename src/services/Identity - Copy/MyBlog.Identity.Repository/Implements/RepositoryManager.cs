﻿//using Infrastructures.Common;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using MyBlog.Identity.Domain.Entities;
//using MyBlog.Identity.Repository.Abstractions;

//namespace MyBlog.Identity.Repository.Implements;
//public class RepositoryManager : UnitOfWork<MyIdentityDbContext>, IRepositoryManager
//{
//    public RepositoryManager(MyIdentityDbContext context) : base(context)
//    {
//    }

//    private IUserRepository _user;
//    private IUserRoleRepository _userRole;
//    private IRoleRepository _role;
//    private IPermissionRepository _permission;

//    public IUserRepository User
//    {
//        get
//        {
//            if (_user == null)
//            {
//                _user = new UserRepository(_context);
//            }

//            return _user;
//        }
//    }

//    public IUserRoleRepository UserRole
//    {
//        get
//        {
//            if (_userRole == null)
//            {
//                _userRole = new UserRoleRepository(_context);
//            }

//            return _userRole;
//        }
//    }

//    public IRoleRepository Role
//    {
//        get
//        {
//            if (_role == null)
//            {
//                _role = new RoleRepository(_context);
//            }

//            return _role;
//        }
//    }

//    public IPermissionRepository Permission
//    {
//        get
//        {
//            if (_permission == null)
//            {
//                _permission = new PermissionRepository(_context);
//            }

//            return _permission;
//        }
//    }

//    //public DbSet<User> Users { get { return _context.Users; } }
//    //public DbSet<Role> Roles { get { return _context.Roles; } }
//    //public DbSet<UserRole> UserRoles { get { return _context.UserRoles; } }
//    //public DbSet<Permission> Permissions { get { return _context.Permissions; } }
//}