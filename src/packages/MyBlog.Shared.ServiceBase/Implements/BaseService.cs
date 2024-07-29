using MyBlog.Shared.ServiceBase.Interfaces;
using AutoMapper;
using ILogger = Serilog.ILogger;

namespace MyBlog.Shared.ServiceBase.Implements;

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