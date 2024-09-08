﻿using Entities = MyBlog.Post.Domain.Entities;
using Contracts.Abstractions.Common;

namespace MyBlog.Post.Repository.Abstractions;

public interface ITagRepository : IRepositoryBase<Entities.Tag, PostDbContext>
{
}