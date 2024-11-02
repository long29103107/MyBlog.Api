//using AutoMapper;
//using FluentValidation;
//using Infrastructures.Common;
//using Microsoft.EntityFrameworkCore;
//using MyBlog.Identity.Repository;
//using MyBlog.Identity.Repository.Abstractions;
//using MyBlog.Identity.Service.Abstractions;

//namespace MyBlog.Identity.Service.Implements;

//public class UserService : BaseIdentityService<IRepositoryManager, MyIdentityDbContext>, IUserService
//{
//    public UserService(IRepositoryManager repoManager, IMapper mapper, IValidatorFactory validatorFactory)
//        : base(repoManager, mapper, validatorFactory)
//    {
//    }

//    public async Task<IEnumerable<int>> GetUserIdsAsync()
//    {
//        return default;// await _repoManager.Permission.FindAll().Select(x => x.Id).ToListAsync();
//    }
//}

