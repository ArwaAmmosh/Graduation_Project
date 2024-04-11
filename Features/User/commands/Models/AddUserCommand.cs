using Azure;
using MediatR;

namespace Graduation_Project.Features.User.commands.Models
{
    public class AddUserCommand : IRequest<Response<string>>
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string University { get; set; }
    }
}
