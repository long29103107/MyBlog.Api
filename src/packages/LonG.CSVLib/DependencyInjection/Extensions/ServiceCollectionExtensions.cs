using LonG.CSVLib.Abstractions;
using LonG.CSVLib.Implements;
using Microsoft.Extensions.DependencyInjection;

namespace LonG.CSVLib.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCSVLibService(this IServiceCollection services)
    {
        services.AddScoped<IFileProccess, FileProccess>();
    }
}