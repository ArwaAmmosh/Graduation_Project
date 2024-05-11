

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteToolsController : ControllerBase
    {
        private readonly UNITOOLDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public FavoriteToolsController(UNITOOLDbContext context,ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        //POST: api/FavoriteTools/{toolId}
        [Authorize]
        [HttpPost("{toolId}")]
        public async Task<IActionResult> AddToFavorites(int toolId)
        {
            var userIdString = _currentUserService.GetUserId() ;
            if (userIdString==null)
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
            var existingFavorite = await _context.FavoriteTool.FirstOrDefaultAsync(ft => ft.UserId == userIdString && ft.ToolId == toolId);
            if (existingFavorite != null)
            {
                return Conflict("Tool already exists in favorites.");
            }

            // Add the tool to the user's favorites
            var favoriteTool = new FavoriteTool { UserId = userIdString, ToolId = toolId };
            _context.FavoriteTool.Add(favoriteTool);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // GET: api/FavoriteTools
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetFavoriteTools(int pageNumber = 1, int pageSize = 10)
        {
            // Retrieve the current user's ID
            var userId = _currentUserService.GetUserId();

            // Find the total count of favorite tools for the current user
            var totalFavoriteToolsCount = await _context.FavoriteTool
                .Where(ft => ft.User.Id == userId)
                .CountAsync();

            // Calculate the number of items to skip
            var skipCount = (pageNumber - 1) * pageSize;

            // Find paginated favorite tools for the current user
            var favoriteToolsQuery = _context.FavoriteTool
                .Include(ft => ft.Tool) // Include the Tool entity
                .Where(ft => ft.User.Id == userId)
                .Select(ft => new FavoriteToolDto
                {
                    ToolId = ft.ToolId,
                    Name = ft.Tool.Name,
                    Description = ft.Tool.Description,
                    RentTime = ft.Tool.RentTime,
                    College = ft.Tool.College,
                    University = ft.Tool.University,
                    Price = ft.Tool.Price,
                    Department = ft.Tool.Department,
                    Photos = _context.ToolPhotos
                        .Where(tp => tp.ToolId == ft.Tool.ToolId)
                        .Select(tp => tp.ToolImages)
                        .ToList()
                })
                .Skip(skipCount)
                .Take(pageSize);

            var paginatedFavoriteTools = await favoriteToolsQuery.ToListAsync();

            if (paginatedFavoriteTools == null || paginatedFavoriteTools.Count == 0)
            {
                return NotFound("Favorite tools not found.");
            }

            return Ok(new { TotalCount = totalFavoriteToolsCount, FavoriteTools = paginatedFavoriteTools });
        }


        // DELETE: api/FavoriteTools/{toolId}
        [Authorize]
        [HttpDelete("{toolId}")]
        public async Task<IActionResult> RemoveFromFavorites(int toolId)
        {
            // Retrieve the current user's ID
            var userId = _currentUserService.GetUserId();
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
        }

    }
}
