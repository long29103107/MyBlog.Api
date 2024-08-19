using Newtonsoft.Json.Linq;
using ValueOf;

namespace MyBlog.Contracts.Domains.ValueOf;

public class CategoryId : ValueOf<int, CategoryId>
{
    protected override void Validate()
    {
        if (Value == 0)
            throw new ArgumentException("Category id connot be null or empty !");
    }
}