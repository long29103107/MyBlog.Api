using AutoMapper;
using Contracts.Abstractions.Common;

namespace Infrastructures.Common;

public abstract class BaseService<TRepoManager> : IBaseService<TRepoManager>
{
    protected readonly TRepoManager _repoManager;
    protected readonly IMapper _mapper;

    protected BaseService(TRepoManager repoManager, IMapper mapper)
    {
        _repoManager = repoManager ?? throw new ArgumentNullException(nameof(_repoManager));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
    }
}