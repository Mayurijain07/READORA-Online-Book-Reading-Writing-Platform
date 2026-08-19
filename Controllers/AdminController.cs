using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReadoraProject.Data;
using ReadoraProject.Interface;
using ReadoraProject.Models;

namespace ReadoraProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _repo;
        public AdminController(IAdminService repo) { _repo = repo; }

        // FEEDBACK ENDPOINTS
        // ==========================================
        [HttpGet("feedback")]
        [ApiExplorerSettings(GroupName = "Feedback")]
        public async Task<IActionResult> GetAllFeedback() => Ok(await _repo.GetFeedbackListAsync());
        
        [HttpPut("mark-read/{id}")]
        [ApiExplorerSettings(GroupName = "Feedback")]
        public async Task<IActionResult> MarkRead(int id)
        {
            // Pehle session check karein
            int? adminId = HttpContext.Session.GetInt32("AdminId");

            // Agar session null hai (jo ki HttpClient call mein hoga hi), 
            // toh default admin 1 use karein taaki database update ho jaye.
            int validAdminId = adminId ?? 1;

            var success = await _repo.UpdateFeedbackStatusAsync(id, validAdminId);

            if (success) return Ok();
            return NotFound();
        }
        
        [HttpDelete("delete/{id}")]
        [ApiExplorerSettings(GroupName = "Feedback")]
        public async Task<IActionResult> DeleteFeedback(int id) => Ok(await _repo.DeleteFeedbackAsync(id));

        // QUERY ENDPOINTS
        // ==========================================
        [HttpGet("queries")]
        [ApiExplorerSettings(GroupName = "Query")] // Swagger mein 'Query' section banayega
        public async Task<IActionResult> GetAllQueries() => Ok(await _repo.GetQueryListAsync());

        // Query resolve karna: Admin ID aur resolution message receive karna
        [HttpPut("resolve-query/{id}")]
        [ApiExplorerSettings(GroupName = "Query")]
        public async Task<IActionResult> ResolveQuery(int id, [FromBody] ResolveRequest request)
        {
            int adminId = HttpContext.Session.GetInt32("AdminId") ?? 1;
            // Repository mein message aur adminId pass karna
            return Ok(await _repo.UpdateQueryStatusAsync(id, adminId, request.message));
        }

        [HttpDelete("delete-query/{id}")]
        [ApiExplorerSettings(GroupName = "Query")]
        public async Task<IActionResult> DeleteQuery(int id) => Ok(await _repo.DeleteQueryAsync(id));
        // Resolve request ke liye helper class
        public class ResolveRequest { public string message { get; set; } }


        // CATEGORY ENDPOINTS
        // ==========================================
        [HttpGet("categories")]
        [ApiExplorerSettings(GroupName = "Category")]
        public async Task<IActionResult> GetAllCategories() => Ok(await _repo.GetCategoryListAsync());

        [HttpPost("add-category")]
        [ApiExplorerSettings(GroupName = "Category")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDetails category)
            => Ok(await _repo.AddCategoryAsync(category));

        [HttpPut("update-category")]
        [ApiExplorerSettings(GroupName = "Category")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDetails category)
            => Ok(await _repo.UpdateCategoryAsync(category));

        [HttpDelete("delete-category/{id}")]
        [ApiExplorerSettings(GroupName = "Category")]
        public async Task<IActionResult> DeleteCategory(int id)
            => Ok(await _repo.DeleteCategoryAsync(id));


        //Users ------
        [HttpGet("users")]
        [ApiExplorerSettings(GroupName = "UserManagement")]
        public async Task<IActionResult> GetAllUsers() => Ok(await _repo.GetUserListAsync());

        [HttpDelete("delete-user/{id}")]
        [ApiExplorerSettings(GroupName = "UserManagement")]
        public async Task<IActionResult> DeleteUser(int id) => Ok(await _repo.DeleteUserAsync(id));

        //CONTENT_______
        [HttpGet("contents")]
        [ApiExplorerSettings(GroupName = "Content")]
        public async Task<IActionResult> GetContents() => Ok(await _repo.GetContentListAsync());

        [HttpPut("update-content-status/{id}/{status}")]
        [ApiExplorerSettings(GroupName = "Content")]
        public async Task<IActionResult> UpdateContentStatus(int id, bool status) {
           
            bool isUpdated = await _repo.UpdateContentStatusAsync(id, status);

            if (isUpdated)
            {
                return Ok(true);
            }
            return BadRequest("Update failed");
        }

        [HttpDelete("delete-content/{id}")]
        [ApiExplorerSettings(GroupName = "Content")]
        public async Task<IActionResult> RemoveContent(int id)
        {
            var result = await _repo.DeleteContentAsync(id);
            return Ok(result);
        }
        //DASHBOARD____
        [HttpGet("dashboard-stats")]
        [ApiExplorerSettings(GroupName = "DashboardStats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            
            var stats = await _repo.GetDashboardStatsAsync();
            return Ok(stats);
        }
    }
    }

