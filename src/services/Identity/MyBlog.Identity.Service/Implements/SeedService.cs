using AutoMapper;
using FluentValidation;
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Repository.Abstractions;
using MyBlog.Identity.Service.Abstractions;
using Newtonsoft.Json;
using Shared.Dtos.Identity.Seed;
using System.Reflection;

namespace MyBlog.Identity.Service.Implements;

public class SeedService : BaseIdentityService, ISeedService
{
    public SeedService(IRepositoryManager repoManager, IMapper mapper, IValidatorFactory validatorFactory)
        : base(repoManager, mapper, validatorFactory)
    {
    }

    public async Task SeedDataAsync(SeedDataRequest request)
    {
        var roles = await _ReadSeedJsonFileAsync<Role>("roles");
        var permissions = await _ReadSeedJsonFileAsync<Permission>("permissions");
        var operations = await _ReadSeedJsonFileAsync<Operation>("operations");
    }

    private async Task<IList<T>> _ReadSeedJsonFileAsync<T>(string fileName)
    {
        var result = new List<T>();
        var rootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        var fullPath = Path.Combine(rootPath, $"Seeds/{fileName}.json");
        using (StreamReader r = new StreamReader(fullPath))
        {
            string json = await r.ReadToEndAsync();
            result = JsonConvert.DeserializeObject<List<T>>(json);
        }

        return result;
    }
}

