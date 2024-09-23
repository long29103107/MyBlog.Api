using Microsoft.EntityFrameworkCore;

namespace Contracts.Abstractions.Common;

public interface IBaseService<TRepoManager, TContext> where TContext : DbContext { }