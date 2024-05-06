using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="SuperAdmin")]
    public class AuthorizationController : AppControllerBase
    {
        [HttpPost("AddNewRole")]
        public async Task<IActionResult> Create([FromForm] AddRoleCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpPost("EditRole")]
        public async Task<IActionResult> Edit([FromForm] EditRoleCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpDelete("DeleteRole/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await Mediator.Send(new DeleteRoleCommand(id));
            return NewResult(response);
        }
        [HttpGet("GetRolesList")]
        public async Task<IActionResult> GetRolesList()
        {
            var response = await Mediator.Send(new GetRolesListQuery());
            return NewResult(response);
        }
        [HttpGet("GetRoleById")]
        public async Task<IActionResult> GetRoleById([FromQuery]int id)
        {
            var response = await Mediator.Send(new GetRoleByIdQuery() { Id = id });
            return NewResult(response);
        }
        [HttpGet("MangeUserRoles")]
        public async Task<IActionResult> MangeUserRoles([FromQuery] int userId)
        {
            var response = await Mediator.Send(new MangeUserRolesQuery() { UserId = userId });
            return NewResult(response);
        }
        [HttpPut("UpdateUserRoles")]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet("MangeUserClaims")]
        public async Task<IActionResult> MangeUserClaimss([FromQuery] int userId)
        {
            var response = await Mediator.Send(new MangeUserClaimsQuery() { UserId = userId });
            return NewResult(response);
        }
        [HttpPut("UpdateUserClaims")]
        public async Task<IActionResult> UpdateUserClaims([FromBody] UpdateUserClaimsCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }
}
