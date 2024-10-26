using AutoMapper;
using Contracts.Abstractions.Common;
using FluentValidation;
using Infrastructures.Common;
using Microsoft.EntityFrameworkCore;
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Repository;
using MyBlog.Identity.Repository.Abstractions;
using MyBlog.Identity.Service.Abstractions;

namespace MyBlog.Identity.Service.Implements;

public class UserService : BaseService<IRepositoryManager, MyIdentityDbContext>, IUserService
{
    public UserService(IRepositoryManager repoManager, IMapper mapper, IValidatorFactory validatorFactory, IUnitOfWork<MyIdentityDbContext> unitOfWork) : base(repoManager, mapper, validatorFactory, unitOfWork)
    {
    }

    public async Task<IEnumerable<int>> GetUserIdsAsync()
    {
        return await _repoManager.User.FindAll().Select(x => x.Id).ToListAsync();
    }
}

