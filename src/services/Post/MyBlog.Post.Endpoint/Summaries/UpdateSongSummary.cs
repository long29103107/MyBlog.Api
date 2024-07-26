using FastEndpoints;
using MyBlog.Post.Service.Dtos.Post;
using LonGMusik.Song.Endpoint.Endpoints;

namespace Customers.Api.Summaries;

public class UpdateSongSummary : Summary<UpdatePostEndpoint>
{
    public UpdateSongSummary()
    {
        Summary = "Updates an existing customer in the system";
        Description = "Updates an existing customer in the system";
        Response<PostResponse>(201, "Customer was successfully updated");
        //Response<ValidationFailureResponse>(400, "The request did not pass validation checks");
    }
}
