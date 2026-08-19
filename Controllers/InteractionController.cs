using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadoraProject.Interface;
using ReadoraProject.Models;

namespace ReadoraProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteractionController : ControllerBase
    {
       
            private readonly IInteractionInterface _repo;

            public InteractionController(IInteractionInterface repo)
            {
                _repo = repo;
            }

            [HttpPost("toggle-follow")]
            public async Task<IActionResult> ToggleFollow(int fId, int tId)
                => Ok(new { msg = await _repo.ToggleFollowAsync(fId, tId) });

            [HttpPost("toggle-like")]
            public async Task<IActionResult> ToggleLike(int uId, int cId)
                => Ok(new { msg = await _repo.ToggleLikeAsync(uId, cId) });

            [HttpPost("toggle-fav")]
            public async Task<IActionResult> ToggleFav(int uId, int cId)
                => Ok(new { msg = await _repo.ToggleFavouriteAsync(uId, cId) });

            [HttpPost("add-comment")]
            public async Task<IActionResult> AddComment([FromBody] CommentDetails comment)
            {
                var result = await _repo.AddCommentAsync(comment);
                return Ok(new { success = result });
            }
        [HttpPost("add-to-history")]
        public async Task<IActionResult> AddToHistory(int uId, int cId)
        {
            // Jab bhi koi story open hogi, ye endpoint call hoga
            var result = await _repo.AddToHistoryAsync(uId, cId);
            return Ok(new { success = result });
        }
    }
    }

