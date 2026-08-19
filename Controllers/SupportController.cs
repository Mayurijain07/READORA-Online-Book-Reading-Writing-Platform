using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadoraProject.Models;
using ReadoraProject.Repository;

namespace ReadoraProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportController : ControllerBase

    {

        private readonly SupportRepository _repository;

        public SupportController(SupportRepository repository)
        {
            _repository = repository;
        }
        [HttpPost("feedback")]
        public async Task<IActionResult> PostFeedback([FromBody] FeedbackDetails feedback)
        {
            if (feedback == null) return BadRequest();

            await _repository.AddFeedbackAsync(feedback);
            return Ok(new { message = "Feedback submitted successfully!" });
        }

        [HttpPost("query")]
        public async Task<IActionResult> PostQuery([FromBody] QueryDetails query)
        {
            if (query == null) return BadRequest();

            await _repository.AddQueryAsync(query);
            return Ok(new { message = "Query posted successfully!" });
        }
        [HttpGet("user-queries/{userId}")]
        public async Task<IActionResult> GetUserQueries(int userId)
        {
            var queries = await _repository.GetUserQueriesAsync(userId);
            return Ok(queries);
        }
    }
}
  