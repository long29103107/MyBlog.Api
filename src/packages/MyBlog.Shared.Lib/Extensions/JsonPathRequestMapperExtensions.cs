using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;

namespace MyBlog.Shared.Lib.Extensions;

public static class JsonPathRequestMapperExtensions
{
    public static TDestination ApplyTo<TSource, TDestination>(this JsonPatchDocument<TSource> patch,
        TDestination destination,
        IMapper mapper) where TSource : class
    {
        try
        {
            var modelToApplyPatch = mapper.Map<TSource>(destination);
            patch.ApplyTo(modelToApplyPatch);
            return mapper.Map<TSource, TDestination>(modelToApplyPatch, destination);
        }
        catch (JsonPatchException jpe)
        {
            throw new Exception("Model is invalid");
        }

    }
}