

namespace Graduation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : AppControllerBase
    {

        [AllowAnonymous]
        [HttpPost("AddnewUser")]
        public async Task<IActionResult> Create([FromForm] AddUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [AllowAnonymous]
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
        public async Task<IActionResult> Update([FromForm] UpdateUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            return NewResult(await Mediator.Send(new DeleteUserCommand(id)));
        }
        [HttpPut("ChangeUserPassword")]
        public async Task<IActionResult> ChangeUserPassword([FromForm] ChangeUserPasswordCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

    }
}
