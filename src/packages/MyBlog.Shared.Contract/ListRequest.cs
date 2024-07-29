using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace LonGBlog.Shared.Contract;

public class ListRequest : Request
{
    [BindNever]
    [JsonIgnore]
    public virtual int Count { get; set; }

    [BindNever]
    [JsonIgnore]
    public virtual List<string> PropertiesWhiteList { get; } = new List<string>();//=> LinqExtensions.GetPropertiesAsString<T>();

    [BindProperty(Name = "fe")]
    public virtual string FilterExp { get; set; } = string.Empty;

    public virtual string Sort { get; set; } = string.Empty;
    
    [BindNever]
    [JsonIgnore]
    public bool OrderDesc
    {
        get
        {
            if (string.IsNullOrEmpty(Sort)) return false;
            Sort = Sort.Trim();
           
            return Sort[0] == '-';
        }
    }

    [BindNever]
    [JsonIgnore]
    public string OrderBy
    {
        get
        {
            if (string.IsNullOrEmpty(Sort)) return null;

            string f = Sort.Trim();

            if (f[0] == '-')
            {
                f = f.Substring(1);
            }

            var validProperties = PropertiesWhiteList.Where(x => string.Equals(f, x, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!validProperties.Any())
            {
                throw new Exception("Invalid sort value. Allow [" + string.Join(", ", PropertiesWhiteList) + "].");
            }

            return f;
        }
    }

    public virtual ListResponse<T> GetListResponse<T>(List<T> Results) where T : class
    {
        ListResponse<T> listResponse = new ListResponse<T>
        {
            Results = Results ?? new List<T>(), // TBD: Return empty list instead null value
            Count = this.Count
        };

        if (listResponse.Count <= 0)
        {
            listResponse.Count = listResponse.Results.Count;
        }

        return listResponse;
    }

    [BindNever]
    [JsonIgnore]
    public bool ToSql { get; set; } = true;
}