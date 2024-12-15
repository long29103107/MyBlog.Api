using AutoMapper;
using Contracts.Domain.Exceptions.Abtractions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Repository.Abstractions;
using MyBlog.Identity.Service.Abstractions;
using Serilog;
using Shared.Dtos.Identity.Register;
using static Shared.Dtos.Identity.UserDtos;

namespace MyBlog.Identity.Service.Implements;

public class RegisterService : BaseIdentityService, IRegisterService
{
    private readonly UserManager<User> _userManager;

    public RegisterService(IRepositoryManager repoManager, IMapper mapper, IValidatorFactory validatorFactory, ILogger logger, UserManager<User> userManager) : base(repoManager, mapper, validatorFactory, logger)
    {
        _userManager = userManager;
    }

    public async Task<UserResponse> RegisterAsync(RegisterRequest request)
    {
        var userExisting = await _userManager.FindByEmailAsync(request.Email);

        if (userExisting is not null)
        {
            if(request.IsSeed)
            {
                return _mapper.Map<UserResponse>(userExisting);
            }
            else
            {
                throw new BadRequestException("User already exists!");
            }
        }
           
        User user = new User()
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new BadRequestException("User creation failed! Please check user details and try again.");
        }

        return _mapper.Map<UserResponse>(user);
    }
}

