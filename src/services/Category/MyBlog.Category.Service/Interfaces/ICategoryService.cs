﻿using MyBlog.Category.Repository.Implements;
using MyBlog.Category.Repository.Interfaces;
using MyBlog.Shared.Databases.Category;
using MyBlog.Shared.ServiceBase.Interfaces;

namespace MyBlog.Category.Service.Interfaces;

public interface ICategoryService : IBaseService<RepositoryManager>
{
    Task<List<ListCategoryResponse>> GetListAsync();
}

