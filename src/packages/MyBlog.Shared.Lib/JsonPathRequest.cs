﻿using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyBlog.Contracts.Dtos.Interfaces;

namespace MyBlog.Shared.Lib;

public class JsonPathRequest<T> : JsonPatchDocument<T>, IRequest where T : class
{
    public JsonPathRequest() : base()
    {
    }

    public JsonPathRequest(List<Microsoft.AspNetCore.JsonPatch.Operations.Operation<T>> operations,
        Newtonsoft.Json.Serialization.IContractResolver contractResolver) : base(operations, contractResolver)
    {
    }

    public IScopedContext? ScopedContext { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    //[JsonIgnore]
    //[BindNever]
    //[SwaggerIgnore]
    //public ScopedContext ScopedContext { get; set; }

    public virtual bool IsDataFormatValidated()
    {
        return true;
    }
}