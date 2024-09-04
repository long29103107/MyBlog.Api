using Microsoft.AspNetCore.Mvc;
using MyBlog.Post.Service.Abstractions;

namespace MyBlog.Post.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public partial class CategoriesController : ControllerBase
{
    //POST /categories: Tạo một danh mục mới.
    //GET /categories/{id}: Lấy thông tin chi tiết về một danh mục.
    //PUT /categories/{id}: Cập nhật thông tin một danh mục.
    //DELETE /categories/{id}: Xóa một danh mục.
    //POST /categories/{id}/ posts: Thêm một bài viết vào danh mục.
    //DELETE /categories/{id}/ posts /{ postId}: Xóa một bài viết khỏi danh mục.

    private readonly IPostService _service;

    public CategoriesController(IPostService service)
    {
        _service = service;
    }
}
