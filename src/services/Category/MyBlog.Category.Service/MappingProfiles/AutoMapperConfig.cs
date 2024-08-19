using AutoMapper;

namespace MyBlog.Category.Service.MappingProfiles;

public static class AutoMapperConfig
{
    public static MapperConfiguration GetMapperConfiguration()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(CategoryServiceReference.AssemblyName);
        });

        return config;
    }
}