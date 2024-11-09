using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyBlog.Identity.Repository.Abstractions;
using MyBlog.Identity.Service.Abstractions;
using Serilog;

namespace MyBlog.Identity.Service.Implements;

public class UserService : BaseIdentityService, IUserService
{
    public UserService(IRepositoryManager repoManager, IMapper mapper, IValidatorFactory validatorFactory, ILogger logger )
        : base(repoManager, mapper, validatorFactory, logger)
    {
    }

    public async Task<IEnumerable<int>> GetUserIdsAsync()
    {
        return await _repoManager.Permission.FindAll().Select(x => x.Id).ToListAsync();
    }
}

