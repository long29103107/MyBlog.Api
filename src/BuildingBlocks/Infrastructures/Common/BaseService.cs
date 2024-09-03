using AutoMapper;
using Contracts.Abstractions.Common;
using ILogger = Serilog.ILogger;

namespace Infrastructures.Common;

public abstract class BaseService<TRepoManager> : IBaseService<TRepoManager>
    where TRepoManager : class
{
    protected TRepoManager _repoManager;
    protected IMapper _mapper;
    //protected ILogger _logger;

    protected BaseService(TRepoManager repoManager, IMapper mapper)//, ILogger logger)
    {
        _repoManager = repoManager;
        _mapper = mapper;
        //_logger = logger;
    }
}