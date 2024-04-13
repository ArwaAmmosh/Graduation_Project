using Graduation_Project.Dtos;
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

        

        // GET: api/Tool
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToolDto>>> GetTools()
        {
            var tools = await _context.Tools
        .Select(t => new ToolDto
        {
            Name = t.Name,
            Description = t.Description,
            RentTime = t.RentTime,
            Price = t.Price,
            College = t.College,
            University = t.University
        })
        .ToListAsync();

            return tools;

        }

        // GET: api/Tool/category/{category}
        [HttpGet("category/{category}")]
            public async Task<ActionResult<IEnumerable<ToolDto>>> GetToolsByCategory(string category)
            {
                var tools = await _context.Tools
                    .Where(tool => tool.Category == category)
                    .Select(t => new ToolDto
                    {
                        Name = t.Name,
                        Description = t.Description,
                        RentTime = t.RentTime,
                        Price = t.Price,
                        College = t.College,
                        University = t.University
                    })
                    .ToListAsync();

                if (tools == null || !tools.Any())
                {
                    return NotFound(); // Return 404 if no tools found for the category
                }

                return tools;
            }
        // GET: api/Tool/category/{category}
        [HttpGet("University/{University}")]
        public async Task<ActionResult<IEnumerable<ToolDto>>> GetToolsByUnivserity(string University)
        {
            var tools = await _context.Tools
                .Where(tool => tool.University == University)
                .Select(t => new ToolDto
                {
                    Name = t.Name,
                    Description = t.Description,
                    RentTime = t.RentTime,
                    Price = t.Price,
                    College = t.College,
                    University = t.University
                })
                .ToListAsync();

            if (tools == null || !tools.Any())
            {
                return NotFound(); // Return 404 if no tools found for the category
            }

            return tools;
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
    }
}

