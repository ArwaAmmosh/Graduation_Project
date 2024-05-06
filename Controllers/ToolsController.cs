using Graduation_Project.Dtos;
using Graduation_Project.Features.Tool.Queries.Models;
using Graduation_Project.Wrapper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolsController : AppControllerBase
    {
        private readonly UNITOOLDbContext _context;
        private readonly CurrentUserService _currentUserService;
        private readonly IToolServices toolServices;
        public ToolsController(UNITOOLDbContext context, CurrentUserService currentUserService, IToolServices _toolServices)
        {
            _context = context;
            _currentUserService = currentUserService; // Initialize _currentUserService
            toolServices = _toolServices;

        }

        //All Tool 
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ToolDto>>> GetTools(int pageNumber = 1, int pageSize = 10)
        {
            var toolsQuery = _context.Tools
                .Select(t => new ToolDto
                {
                    Name = t.Name,
                    Description = t.Description,
                    RentTime = t.RentTime,
                    Price = t.Price,
                    College = t.College,
                    University = t.University
                })
                .ToList(); // Materialize the query into a list

            var paginatedTools = PaginatedResult<ToolDto>.Success(toolsQuery, toolsQuery.Count(), pageNumber, pageSize);

            return Ok(paginatedTools);

            //var response = await Mediator.Send(query);
            //return Ok(response);
        }



        // GET: api/Tool/category/{category}
        [HttpGet("category/{category}")]
        public async Task<ActionResult<PaginatedResult<ToolDto>>> GetToolsByCategory(string category, int pageNumber = 1, int pageSize = 10)
        {
            var toolsQuery = _context.Tools
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
                .ToList(); // Materialize the query into a list

            var paginatedTools = PaginatedResult<ToolDto>.Success(toolsQuery, toolsQuery.Count(), pageNumber, pageSize);

            return Ok(paginatedTools);
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
        public async Task<ActionResult<PaginatedResult<ToolDto>>> SearchToolsByName(string name, int pageNumber = 1, int pageSize = 10)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Tool name cannot be empty.");
            }

            var toolsQuery = _context.Tools
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
                .ToList(); // Materialize the query into a list

            var paginatedTools = PaginatedResult<ToolDto>.Success(toolsQuery, toolsQuery.Count(), pageNumber, pageSize);

            return Ok(paginatedTools);
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
        public async Task<ActionResult<Tool>> PostTool([FromForm] ToolPostDto toolPostDto)
        {
           var image1 = await toolServices.Upload1Image(toolPostDto.ToolImages1);
            var image2 = await toolServices.Upload2Image(toolPostDto.ToolImages2);
            var image3 = await toolServices.Upload3Image(toolPostDto.ToolImages3);
            var image4 = await toolServices.Upload4Image(toolPostDto.ToolImages4);

            var userId =_currentUserService.GetUserId();
            if (userId != null)
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
                    UserId = userId,
                    Department= toolPostDto.Department,
                    ToolImages1=image1,
                    ToolImages2 = image2,
                    ToolImages4=image4,
                    ToolImages3 = image3



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

