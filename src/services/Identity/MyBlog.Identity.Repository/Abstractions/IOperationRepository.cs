using Contracts.Abstractions.Common;
using MyBlog.Identity.Domain.Entities;

namespace MyBlog.Identity.Repository.Abstractions;

public interface IOperationRepository : IRepositoryIdentityBase<Operation, MyIdentityDbContext>
{ }