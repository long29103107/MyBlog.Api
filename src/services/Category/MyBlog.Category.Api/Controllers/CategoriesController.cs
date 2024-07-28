using Microsoft.AspNetCore.Mvc;
using MyBlog.Category.Service.Interfaces;

namespace MyBlog.Category.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;
    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync()
    {
        return Ok(await _service.GetListAsync());
    }
}
