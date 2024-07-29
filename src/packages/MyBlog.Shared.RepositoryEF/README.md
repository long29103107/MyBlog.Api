# General generic repository
This libary will help me add repository to project quickly

## Add generic repository:
```
// Inteface
public interface ISongRespository : IRepositoryBase<Model.Song, SongDbContext>
{
   
}


//Implement
public class SongRespository :RepositoryBase<Model.Song, SongDbContext>, ISongRespository
{
    public SongRespository(SongDbContext context, IUnitOfWork<SongDbContext> unitOfWork) : base(context, unitOfWork)
    {

    }
}
```

## Add repository manager:
```
// Inteface
public interface IRepositoryManager : IRepositoryManagerBase<SongDbContext>
{
    public ISongRespository Song { get; }

    DbSet<Model.Song> Songs { get; }
}



//Implement
public class RepositoryManager : RepositoryManagerBase<SongDbContext>, IRepositoryManager
{
    public RepositoryManager(IUnitOfWork<SongDbContext> unitOfWork, SongDbContext context) : base(unitOfWork, context)
    {

    }

    private ISongRespository _song;

    public ISongRespository Song
    {
        get
        {
            if (_song == null)
            {
                _song = new SongRespository(_context, _unitOfWork);
            }

            return _song;
        }
    }

    public DbSet<Model.Song> Songs
    {
        get
        {
            return _context.Songs;
        }
    }
}

```

## Add repository manager:
```
//Add service to dependency injection
builder.Services.AddScoped<ISongRespository, SongRespository>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddGenericRepository();
```