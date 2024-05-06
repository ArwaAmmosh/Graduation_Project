using Graduation_Project.Features.Emails.Command.Models;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class EmailsController : AppControllerBase
    {

            [HttpPost("SendEmail")]
            public async Task<IActionResult> SendEmail([FromQuery] SendEmailCommand command)
            {
                var response = await Mediator.Send(command);
                return NewResult(response);
            }

        }
    }
