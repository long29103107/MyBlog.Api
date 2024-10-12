using Microsoft.AspNetCore.Mvc;
using Shared.APIs;

namespace MyBlog.Idenity.Api.Controllers;

public class TestController : CustomControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetListAsync()
    { 
        return Ok("Test controller successfully !");
    }
}
