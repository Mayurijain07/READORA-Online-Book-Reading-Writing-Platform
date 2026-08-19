using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadoraProject.Models;
using ReadoraProject.Services;

namespace ReadoraProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryApiService _service;

        // Dependency Injection: Service yahan inject hogi
        public CategoryController(CategoryApiService service)
        {
            _service = service;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDetails>>> GetCategories()
        {
            var categories = await _service.GetAllCategoriesAsync();

            if (categories == null)
            {
                return NotFound("No categories found.");
            }

            return Ok(categories);
        }
          // URL: api/Category/stories/{id}
        [HttpGet("stories/{id}")]
        public async Task<ActionResult<IEnumerable<ContentDetails>>> GetStoriesByCategory(int id)
        {
            var stories = await _service.GetStoriesByCategory(id);

            if (stories == null || !stories.Any())
            {
                return NotFound($"No stories found for category ID: {id}");
            }

            return Ok(stories);
        }

    }
}

   
