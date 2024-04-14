using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Graduation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : AppControllerBase
    {
        [HttpPost("AddNewUser")]
        public async Task<IActionResult> Create([FromBody] AddUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet("Pagination")]
        public async Task<IActionResult> Paginated([FromQuery] GetUserPaginationQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetUserById([FromQuery] int id)
        {
            var response = await Mediator.Send(new GetUserByIdQuery(id));
            return Ok(response);
        }
        [HttpPut("UpdateUserInformation")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

    }
}
