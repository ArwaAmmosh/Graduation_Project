using Graduation_Project.Entities.Identity;
using Graduation_Project.Wrapper;

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
        [HttpGet("Pagination")]
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
                    University = t.University,
                    ToolImages1 = t.ToolImages1,
                    ToolImages2 = t.ToolImages2,
                    ToolImages3 = t.ToolImages3,
                    ToolImages4 = t.ToolImages4,
                    Department = t.Department
                })
                .ToList(); // Materialize the query into a list

            var paginatedTools = PaginatedResult<ToolDto>.Success(toolsQuery, toolsQuery.Count(), pageNumber, pageSize);

            return Ok(paginatedTools);


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
                    University = t.University,
                    ToolImages1 = t.ToolImages1,
                    ToolImages2 = t.ToolImages2,
                    ToolImages3 = t.ToolImages3,
                    ToolImages4 = t.ToolImages4,
                    Department = t.Department

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
                    University = t.University,
                    ToolImages1 = t.ToolImages1,
                    ToolImages2 = t.ToolImages2,
                    ToolImages3 = t.ToolImages3,
                    ToolImages4 = t.ToolImages4,
                    Department = t.Department


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
                    University = t.University,
                    ToolImages1 = t.ToolImages1,
                    ToolImages2 = t.ToolImages2,
                    ToolImages3 = t.ToolImages3,
                    ToolImages4 = t.ToolImages4,
                    Department = t.Department

                })
                .ToList(); // Materialize the query into a list

            var paginatedTools = PaginatedResult<ToolDto>.Success(toolsQuery, toolsQuery.Count(), pageNumber, pageSize);

            return Ok(paginatedTools);
        }

        //Update //// PUT: api/Tool/{id}
        [HttpPut("UpdateTool/{id}")]
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
            existingTool.ToolImages1 = updateToolDto.ToolImages1;
            existingTool.ToolImages2 = updateToolDto.ToolImages2;
            existingTool.ToolImages3 = updateToolDto.ToolImages3;
            existingTool.ToolImages4 = updateToolDto.ToolImages4;

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
        
        [HttpDelete("DeleteTool/{id}")]
        public async Task<IActionResult> DeleteTool(int id)
        {
            var tools =_context.FavoriteTool.Where(t => t.ToolId == id).ToList();
            foreach (var tool in tools)
            {
                _context.FavoriteTool.Remove(tool);
            }
            await _context.SaveChangesAsync();
            try
            {
                var tool = await _context.Tools.FindAsync(id);
                if (tool == null)
                {
                    return NotFound();
                }

                _context.Tools.Remove(tool);
                await _context.SaveChangesAsync();

                return NoContent(); // Return 204 No Content on successful deletion
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Log the error or handle it appropriately
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while deleting the tool.");
            }
        }
        //GET tool user ADD
        [Authorize]
        [HttpGet("GetUserTools")]
        public async Task<ActionResult<IEnumerable<Tool>>> GetMyTools()
        {
            var userId = _currentUserService.GetUserId();

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
        [HttpPost("AddnewTool")]
        [Authorize]
        public async Task<ActionResult<Tool>> PostTool([FromForm] ToolPostDto toolPostDto)
        {
            var image1 = await toolServices.Upload1Image(toolPostDto.ToolImages1);
            var image2 = await toolServices.Upload2Image(toolPostDto.ToolImages2);
            var image3 = await toolServices.Upload3Image(toolPostDto.ToolImages3);
            var image4 = await toolServices.Upload4Image(toolPostDto.ToolImages4);

            var userId =_currentUserService.GetUserId();
            var user = await _currentUserService.GetUserAsync();
            if (user != null)
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
                    Department = toolPostDto.Department,
                    ToolImages1 = image1,
                    ToolImages2 = image2,
                    ToolImages4 = image4,
                    ToolImages3 = image3,
                    UserId=userId
                   



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

