using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestModeController : AppControllerBase
    {
        [HttpPost("AddNewGuestInfo")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromForm] AddNewGuestInfoCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete("DeleteGuestInfo/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return NewResult(await Mediator.Send(new DeleteGuestInfoCommand() { Id=id}));

        }
        [HttpGet("GetGuestInfo")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            return NewResult(await Mediator.Send(new GetGuestByIdQuery() { Id = id }));

        }

    }
}
