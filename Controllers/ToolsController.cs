using Graduation_Project.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolsController : ControllerBase
    {
        private readonly UNITOOLDbContext _context;
        private readonly CurrentUserService _currentUserService;

        public ToolsController(UNITOOLDbContext context, CurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService; // Initialize _currentUserService
        }

        //All Tool 
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
                return NotFound();
            }

            return tools;
        }

        // GET: api/Tool/University/{University}
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
                return NotFound();
            }

            return tools;
        }

        // Search  // GET: api/Tool/search?name={Name}
        [HttpGet("searchByName")]
        public async Task<ActionResult<IEnumerable<ToolDto>>> SearchToolsByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Tool name cannot be empty.");
            }

            var tools = await _context.Tools
                .Where(tool => tool.Name.Contains(name))
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
                return NotFound();
            }

            return tools;
        }

        //Update //// PUT: api/Tool/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTool(int id, UpdateToolDto updateToolDto)
        {
            if (id != updateToolDto.ToolId)
            {
                return BadRequest("Invalid tool ID.");
            }

            var existingTool = await _context.Tools.FindAsync(id);
            if (existingTool == null)
            {
                return NotFound("Tool not found.");
            }

            existingTool.Name = updateToolDto.Name;
            existingTool.Description = updateToolDto.Description;
            existingTool.RentTime = updateToolDto.RentTime;
            existingTool.Category = updateToolDto.Category;
            existingTool.Price = updateToolDto.Price;
            existingTool.College = updateToolDto.College;
            existingTool.University = updateToolDto.University;
            existingTool.Acadmicyear = updateToolDto.Acadmicyear;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToolExists(id))
                {
                    return NotFound("Tool not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Tools/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTool(int id)
        {
            var tool = await _context.Tools.FindAsync(id);
            if (tool == null)
            {
                return NotFound();
            }

            _context.Tools.Remove(tool);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //GET tool user ADD
        [Authorize]
        [HttpGet("mytools")]
        public async Task<ActionResult<IEnumerable<Tool>>> GetMyTools()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // Retrieve tools associated with the current user
            var userTools = await _context.Tools.Where(t => t.UserId == userId).ToListAsync();

            return Ok(userTools);
        }
        // GET: api/Tools/{id}
        [HttpGet("{id}", Name = "GetTool")]
        public async Task<ActionResult<Tool>> GetTool(int id)
        {
            var tool = await _context.Tools.FindAsync(id);
            if (tool == null)
            {
                return NotFound();
            }

            return tool;
        }
        // POST: api/Tools
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Tool>> PostTool(ToolPostDto toolPostDto)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            var userIdClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                // Map properties from DTO to Tool entity
                var tool = new Tool
                {
                    Name = toolPostDto.Name,
                    Description = toolPostDto.Description,
                    RentTime = toolPostDto.RentTime,
                    College = toolPostDto.College,
                    University = toolPostDto.University,
                    Category = toolPostDto.Category,
                    Price = toolPostDto.Price,
                    Acadmicyear = toolPostDto.Acadmicyear,
                    UserId = userId
                };

                _context.Tools.Add(tool);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse(200));
            }
            else
            {
                return BadRequest(new ApiResponse(400));
            }
        }

        private bool ToolExists(int id)
        {
            return _context.Tools.Any(e => e.ToolId == id);
        }
    }
}