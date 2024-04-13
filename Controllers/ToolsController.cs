using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class ToolsController : ControllerBase
        {
            private readonly UNITOOLDbContext _context;

            public ToolsController(UNITOOLDbContext context)
            {
                _context = context;
            }

            // POST: api/Tool
            [HttpPost]
            public async Task<ActionResult<Tool>> PostTool(Tool tool)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Add the tool to the database
                _context.Tools.Add(tool);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTool", new { id = tool.ToolId }, tool);
            }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tool>>> GetTools()
        {
            var tools = await _context.Tools.ToListAsync();
            return tools;
        }

        // GET: api/Tool/category/{category}
        [HttpGet("category/{category}")]
            public async Task<ActionResult<IEnumerable<Tool>>> GetToolsByCategory(string category)
            {
                var tools = await _context.Tools
                    .Where(tool => tool.Category == category)
                    .ToListAsync();

                if (tools == null || !tools.Any())
                {
                    return NotFound(); // Return 404 if no tools found for the category
                }

                return tools;
            }
        }
}

