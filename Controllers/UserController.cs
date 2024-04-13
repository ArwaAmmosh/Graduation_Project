using Graduation_Project.Bases;
using Graduation_Project.Features.Users.commands.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;

namespace Graduation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController  : AppControllerBase
    {
        [HttpPost("AddNewUser")]
        public async Task<IActionResult> Create([FromBody] AddUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }
}
