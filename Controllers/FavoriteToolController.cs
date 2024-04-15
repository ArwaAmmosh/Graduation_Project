using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Graduation_Project.Dtos;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteToolsController : ControllerBase
    {
        private readonly UNITOOLDbContext _context;

        public FavoriteToolsController(UNITOOLDbContext context)
        {
            _context = context;
        }

        /* POST: api/FavoriteTools/{toolId}
        [Authorize]
        [HttpPost("{toolId}")]
        public async Task<IActionResult> AddToFavorites(int toolId)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out int userId))
            {
                // Handle the case where the user ID cannot be parsed as an integer
                return BadRequest("Invalid user ID format.");
            }

            // Check if the tool exists
            var tool = await _context.Tools.FindAsync(toolId);
            if (tool == null)
            {
                return NotFound("Tool not found.");
            }

            // Check if the tool is already in the user's favorites
            var existingFavorite = await _context.FavoriteTool
                .FirstOrDefaultAsync(ft => ft.UserId == userId && ft.ToolId == toolId);
            if (existingFavorite != null)
            {
                return Conflict("Tool already exists in favorites.");
            }

            // Add the tool to the user's favorites
            var favoriteTool = new FavoriteTool { UserId = userId, ToolId = toolId };
            _context.FavoriteTool.Add(favoriteTool);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // GET: api/FavoriteTools
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetFavoriteTool()
        {
            // Retrieve the current user's ID
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out int userId))
            {
                // Handle the case where the user ID cannot be parsed as an integer
                return BadRequest("Invalid user ID format.");
            }

            // Find the user's favorite tool
            var favoriteTool = await _context.FavoriteTool
                .Include(ft => ft.Tool) // Include the Tool entity
                .FirstOrDefaultAsync(ft => ft.UserId == userId);

            if (favoriteTool == null)
            {
                return NotFound("Favorite tool not found.");
            }

            // Return the favorite tool DTO
            var favoriteToolDto = new FavoriteToolDto
            {
                ToolId = favoriteTool.ToolId,
                Name = favoriteTool.Tool.Name, // Assuming Tool has a Name property
                                                   // Include other properties of the Tool entity as needed
            };

            return Ok(favoriteToolDto);
        }

        // DELETE: api/FavoriteTools/{toolId}
        [Authorize]
        [HttpDelete("{toolId}")]
        public async Task<IActionResult> RemoveFromFavorites(int toolId)
        {
            // Retrieve the current user's ID
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out int userId))
            {
                // Handle the case where the user ID cannot be parsed as an integer
                return BadRequest("Invalid user ID format.");
            }

            // Find the favorite tool entry for the user and tool
            var favoriteTool = await _context.FavoriteTool
                .FirstOrDefaultAsync(ft => ft.UserId == userId && ft.ToolId == toolId);
            if (favoriteTool == null)
            {
                return NotFound("Favorite tool not found.");
            }

            // Remove the tool from the user's favorites
            _context.FavoriteTool.Remove(favoriteTool);
            await _context.SaveChangesAsync();

            return NoContent(); 
        }*/
    }
}
