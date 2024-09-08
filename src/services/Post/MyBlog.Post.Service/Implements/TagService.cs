
using AutoMapper;
using Contracts.Abstractions.Common;
using FluentValidation;
using Infrastructures.Common;
using MyBlog.Post.Repository.Abstractions;
using MyBlog.Post.Repository;
using MyBlog.Post.Service.Abstractions;
using static Shared.Dtos.Post.TagDtos;
using Microsoft.EntityFrameworkCore;
using Entities = MyBlog.Post.Domain.Entities;
using Contracts.Domain.Exceptions;
using Exceptions = Contracts.Domain.Exceptions;
using MyBlog.Post.Domain.Exceptions;

namespace MyBlog.Post.Service.Implements;

public class TagService : BaseService<IRepositoryManager, PostDbContext>, ITagService
{
    public TagService(
        IRepositoryManager repoManager
        , IMapper mapper
        , IValidatorFactory validatorFactory
        , IUnitOfWork<PostDbContext> unitOfWork
        )
        : base(repoManager, mapper, validatorFactory, unitOfWork)
    {

    }

    public async Task<TagResponse> CreateAsync(TagCreateRequest request)
    {
        //var model = _mapper.Map<Entities.Tag>(request);

        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<TagResponse> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TagResponse>> GetListAsync(TagListRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TagResponse>> GetTagsFromPost(int postId)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveTagFromPostsAsync(int categoryId, int postId)
    {
        throw new NotImplementedException();
    }

    public async Task<TagResponse> UpdateAsync(int id, TagUpdateRequest category)
    {
        throw new NotImplementedException();
    }

    private async Task<Entities.Tag> _InternalGetTagAsync(int id)
        => await _repoManager.Tag
           .FindByCondition(x => x.Id == id)
           .Include(c => c.Posts) 
           .FirstOrDefaultAsync()
            ?? throw new TagException.NotFound(id);

    private async Task _ValidateProductAsync(Entities.Post model)
    {
        var validator = _validatorFactory.GetValidator<Entities.Post>();
        var result = await validator.ValidateAsync(model);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage)).ToList();

            throw new Exceptions.ValidationException(errors);
        }
    }
}
