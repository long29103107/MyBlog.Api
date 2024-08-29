using AutoMapper;
using MyBlog.Category.Service;

namespace MyBlog.Post.Service.MappingProfiles;

public static class AutoMapperConfig
{
    public static MapperConfiguration GetMapperConfiguration()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(PostServiceReference.AssemblyName);
        });

        return config;
    }
}