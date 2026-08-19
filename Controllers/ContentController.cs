using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadoraProject.Models;
using ReadoraProject.Services;

namespace ReadoraProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly ContentApiService _service;
        public ContentController(ContentApiService service) => _service = service;

        [HttpPost("publish")]
        public async Task<IActionResult> Publish([FromForm] ContentDetails content, IFormFile? file)
        {
            await _service.SaveContentAsync(content, file);
            return Ok(new { message = "Content saved successfully" });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContent(int id)
        {
            var content = await _service.GetContentAsync(id);
            if (content == null) return NotFound();
            return Ok(content);
        }
    }
}
