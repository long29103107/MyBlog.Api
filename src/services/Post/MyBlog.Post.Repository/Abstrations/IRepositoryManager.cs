﻿using Contracts.Abstractions.Common;
using Microsoft.EntityFrameworkCore;
using MyBlog.Post.Repository.Abstractions;
using Entities = MyBlog.Post.Domain.Entities;

namespace MyBlog.Post.Repository.Abstractions;

public interface IRepositoryManager : IUnitOfWork<PostDbContext>
{
    public IPostRepository Post { get; }

    DbSet<Entities.Post> Posts { get; }
}