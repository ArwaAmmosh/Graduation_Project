using Graduation_Project.Entities.Identity;
using Graduation_Project.Wrapper;
using System.Text.Json.Serialization;
using System.Text.Json;
using Graduation_Project.Entities;


namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolsController : AppControllerBase
    {
        private readonly UNITOOLDbContext _context;
        private readonly CurrentUserService _currentUserService;
        private readonly IWebHostEnvironment _env;
        private readonly IFileService _fileService;
        private readonly string _host = "https://unitoolproject.runasp.net/";


        public ToolsController(UNITOOLDbContext context, CurrentUserService currentUserService, IWebHostEnvironment env,IFileService fileService)
        {
            _context = context;
            _currentUserService = currentUserService; 
            _env = env;
            _fileService = fileService;

        }
        // GET: api/Tools/{id}
        [HttpGet("{id}", Name = "GetTool")]
        public async Task<ActionResult<GetToolDto>> GetTool(int id)
        {
            var tool = await _context.Tools.FindAsync(id);
            if (tool == null)
            {
                return NotFound();
            }
            var getToolDto = new GetToolDto
            {
                ToolId = tool.ToolId,
                Name = tool.Name,
                Description = tool.Description,
                RentTime = tool.RentTime,
                Price = tool.Price,
                College = tool.College,
                University = tool.University,
                Department = tool.Department,
                Acadmicyear=tool.AcademicYear,
                Photos = _context.ToolPhotos
                                         .Where(tp => tp.ToolId == tool.ToolId)
                                         .Select(tp => tp.ToolImages)
                                          .ToList()
            };

            return getToolDto;


        }
        //All Tool
        [HttpGet("AllToolWithoutAuthorized")]
        public async Task<ActionResult<PaginatedResult<GetToolDto>>> GetTools(int pageNumber = 1, int pageSize = 10)
        {
            var toolsQuery = _context.Tools
                .Select(t => new GetToolDto
                {
                    ToolId = t.ToolId,
                    Name = t.Name,
                    Description = t.Description,
                    RentTime = t.RentTime,
                    Price = t.Price,
                    College = t.College,
                    University = t.University,
                    Photos = _context.ToolPhotos
                        .Where(tp => tp.ToolId == t.ToolId)
                        .Select(tp => tp.ToolImages)
                        .ToList()
                })
                .ToList(); // Materialize the query into a list

            var paginatedTools = PaginatedResult<GetToolDto>.Success(toolsQuery, toolsQuery.Count(), pageNumber, pageSize);

            return Ok(paginatedTools);
        }

        //GET tool user ADD
        [Authorize]
        [HttpGet("GetUserTools")]
        public async Task<ActionResult<PaginatedResult<GetToolDto>>> GetMyTools(int pageNumber = 1, int pageSize = 10)
        {
            var userId = _currentUserService.GetUserId();

            // Retrieve tools associated with the current user
            var toolsQuery = _context.Tools
                .Where(t => t.UserId == userId)
                .Select(t => new GetToolDto
                {
                    ToolId = t.ToolId,
                    Name = t.Name,
                    Description = t.Description,
                    RentTime = t.RentTime,
                    Price = t.Price,
                    College = t.College,
                    University = t.University,
                    Photos = _context.ToolPhotos
                        .Where(tp => tp.ToolId == t.ToolId)
                        .Select(tp => tp.ToolImages)
                        .ToList()
                })
                .ToList();

            // Count the total number of tools associated with the current user
            var userToolsCount = await _context.Tools
                .Where(t => t.UserId == userId)
                .CountAsync();

            var paginatedTools = PaginatedResult<GetToolDto>.Success(toolsQuery, userToolsCount, pageNumber, pageSize);

            return Ok(paginatedTools);
        }


        // GET: api/Tool/category/{category}
        [HttpGet("ToolByCategory")]
        public async Task<ActionResult<PaginatedResult<GetToolDto>>> GetToolsByCategory(string category, int pageNumber = 1, int pageSize = 10)
        {
            var toolsQuery = _context.Tools
                .Where(tool => tool.Category == category)
                .Select(t => new GetToolDto
                {
                    ToolId = t.ToolId,
                    Name = t.Name,
                    Description = t.Description,
                    RentTime = t.RentTime,
                    Price = t.Price,
                    College = t.College,
                    University = t.University,
                    Photos = _context.ToolPhotos
                        .Where(tp => tp.ToolId == t.ToolId)
                        .Select(tp => tp.ToolImages)
                        .ToList()

                })
                .ToList(); // Materialize the query into a list

            var paginatedTools = PaginatedResult<GetToolDto>.Success(toolsQuery, toolsQuery.Count(), pageNumber, pageSize);

            return Ok(paginatedTools);
        }

        // GET: api/Tool/University/{University}
        [HttpGet("ToolByUniversity")]
        public async Task<ActionResult<PaginatedResult<GetToolDto>>> GetToolsByUnivserity(string University, int pageNumber = 1, int pageSize = 10)
        {
            var toolsQuery =  _context.Tools
                .Where(tool => tool.University == University)
                .Select(t => new GetToolDto
                {

                    ToolId = t.ToolId,
                    Name = t.Name,
                    Description = t.Description,
                    RentTime = t.RentTime,
                    Price = t.Price,
                    College = t.College,
                    University = t.University,
                    Acadmicyear=t.AcademicYear,
                    Photos = _context.ToolPhotos
                        .Where(tp => tp.ToolId == t.ToolId)
                        .Select(tp => tp.ToolImages)
                        .ToList()

                })
                .ToList();
            var paginatedTools = PaginatedResult<GetToolDto>.Success(toolsQuery, toolsQuery.Count(), pageNumber, pageSize);

            return Ok(paginatedTools);
        }
        // GET: api/Tools/categories
        [HttpGet("categories")]
        public async Task<ActionResult<List<string>>> GetToolCategories()
        {
            var categories = await _context.Tools
                .Select(t => t.Category)
                .Distinct()
                .ToListAsync();

            if (categories == null || categories.Count == 0)
            {
                return NotFound("No categories found.");
            }

            return Ok(categories);
        }

        // Search  // GET: api/Tool/search?name={Name}
        [HttpGet("searchByName")]
        public async Task<ActionResult<PaginatedResult<GetToolDto>>> SearchToolsByName(string name, int pageNumber = 1, int pageSize = 10)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Tool name cannot be empty.");
            }

            var toolsQuery = _context.Tools
                .Where(tool => tool.Name.Contains(name))
                .Select(t => new GetToolDto
                {

                    ToolId = t.ToolId,
                    Name = t.Name,
                    Description = t.Description,
                    RentTime = t.RentTime,
                    Price = t.Price,
                    College = t.College,
                    University = t.University,
                    Department=t.Department,
                    Acadmicyear=t.AcademicYear,
                    Photos = _context.ToolPhotos
                        .Where(tp => tp.ToolId == t.ToolId)
                        .Select(tp => tp.ToolImages)
                        .ToList()
                })
                .ToList(); // Materialize the query into a list

            var paginatedTools = PaginatedResult<GetToolDto>.Success(toolsQuery, toolsQuery.Count(), pageNumber, pageSize);

            return Ok(paginatedTools);
        }

        [HttpPut("UpdateTool/{id}")]
        public async Task<IActionResult> UpdateTool(int id, [FromForm] UpdateToolDto updateToolDto)
        {
            // Retrieve the existing tool from the database
            var existingTool = await _context.Tools.FindAsync(id);
            if (existingTool == null)
            {
                return NotFound("Tool not found.");
            }

            // Update properties from DTO
            existingTool.Name = updateToolDto.Name;
            existingTool.Description = updateToolDto.Description;
            existingTool.RentTime = updateToolDto.RentTime;
            existingTool.Price = updateToolDto.Price;
            existingTool.University = updateToolDto.University;
            existingTool.Department = updateToolDto.Department;
            existingTool.Category = updateToolDto.Category;
            existingTool.AcademicYear = updateToolDto.Acadmicyear;

            // Handle photo updates
            if (updateToolDto.Photos != null && updateToolDto.Photos.Any())
            {
                // Delete existing photos if needed (optional)
                var existingPhotos = _context.ToolPhotos.Where(tp => tp.ToolId == id);
                _context.ToolPhotos.RemoveRange(existingPhotos);

                // Save new photos
                foreach (var photoFile in updateToolDto.Photos)
                {
                    if (photoFile != null && photoFile.Length > 0)
                    {
                        // Process each photo file
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);
                        var filePath = Path.Combine(_env.WebRootPath, "ToolImages", fileName);

                        // Save the photo to the uploads directory in wwwroot
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await photoFile.CopyToAsync(stream);
                        }

                        // Create ToolPhoto entity
                        var toolPhoto = new ToolPhoto
                        {
                            ToolId = id,
                            ToolImages = "/ToolImages/" + fileName // Save relative path to access the photo from the web
                        };

                        // Add tool photo to the database
                        _context.ToolPhotos.Add(toolPhoto);
                    }
                }
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/Tools/{id}

        [HttpDelete("DeleteTool/{id}")]
        [Authorize]
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
      

       
        // POST: api/Tools
        [HttpPost("AddnewTool")]
        [Authorize]
        public async Task<ActionResult<Tool>> PostTool([FromForm] PostToolDto toolPostDto)
        {
            var userId = _currentUserService.GetUserId();
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
                    AcademicYear = toolPostDto.Acadmicyear,
                    Department = toolPostDto.Department,
                    UserId = userId
                };

                // Add tool to the database
                _context.Tools.Add(tool);
                await _context.SaveChangesAsync();

                // Save tool photos if provided
                if (toolPostDto.Photos != null && toolPostDto.Photos.Any())
                {
                    foreach (var photo in toolPostDto.Photos)
                    {
                        if (photo != null && photo.Length > 0)
                        {
                            // Create a unique file name to prevent conflicts


                            // Create ToolPhoto entity
                            var toolPhoto = new ToolPhoto
                            {
                                ToolId = tool.ToolId, // Assuming ToolId is generated by the database
                                ToolImages =await _fileService.UploadImage("ToolImages", photo)// Save relative path to access the photo from the web
                            };

                            // Add tool photo to the database
                            _context.ToolPhotos.Add(toolPhoto);
                        }
                    }
                    await _context.SaveChangesAsync();
                }

                return Ok(new ApiResponse(200));
            }
            else
            {
                return BadRequest(new ApiResponse(400));
            }
        }

        [Authorize]
        [HttpGet("dependonuservalue")]
        public async Task<ActionResult<PaginatedResult<GetToolDto>>> GetToolsForUser(int pageNumber = 1, int pageSize = 10)
        {
            // Retrieve the current user's ID from the token 
            var userId = _currentUserService.GetUserId();
            // Retrieve user based on userId
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            // Filter tools based on user attributes
            var toolsQuery = _context.Tools.AsQueryable();
            if (!string.IsNullOrEmpty(user.University))
            {
                toolsQuery = toolsQuery.Where(t => t.University == user.University);
            }
            if (!string.IsNullOrEmpty(user.College))
            {
                toolsQuery = toolsQuery.Where(t => t.College == user.College);
            }
            if (!string.IsNullOrEmpty(user.Department))
            {
                toolsQuery = toolsQuery.Where(t => t.Department == user.Department);
            }
            if (!string.IsNullOrEmpty(user.AcademicYear))
            {
                toolsQuery = toolsQuery.Where(t => t.AcademicYear == user.AcademicYear);
            }

            // Paginate the filtered tools
            var totalItems = await toolsQuery.CountAsync();
            var paginatedTools = await toolsQuery
                .Select(t => new GetToolDto
                {
                    ToolId = t.ToolId,
                    Name = t.Name,
                    Description = t.Description,
                    RentTime = t.RentTime.ToString(), // Assuming RentTime is string in GetToolDto
                    College = t.College,
                    University = t.University,
                    Price = t.Price,
                    Department = t.Department,
                    Acadmicyear=t.AcademicYear,
                    Photos = _context.ToolPhotos
                        .Where(tp => tp.ToolId == t.ToolId)
                        .Select(tp => tp.ToolImages)
                        .ToList()
                })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var paginatedResult = PaginatedResult<GetToolDto>.Success(paginatedTools, totalItems, pageNumber, pageSize);
            return Ok(paginatedResult);
        }

        private bool ToolExists(int id)
        {
            return _context.Tools.Any(e => e.ToolId == id);
        }


    }

}

