using Microsoft.AspNetCore.Mvc;
using MyBlog.Category.Service.Interfaces;

namespace MyBlog.Category.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;
   // private readonly IConfiguration _config;
    public CategoriesController(ICategoryService service)//, IConfiguration config)
    {
        _service = service;
       //_config = config;

        //var aa = _config["ConnectionStrings:DefaultConnection"];
    }

    //[HttpPost("seed")]
    //public async Task<IActionResult> SeedDataAsync()
    //{
    //    await _service.SeedDataAsync();
    //    return Ok("Seeding data successfully");
    //}

    [HttpGet]
    public async Task<IActionResult> GetListAsync()
    {
        return Ok(await _service.GetListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        return Ok(await _service.GetAsync(id));
    }
}
