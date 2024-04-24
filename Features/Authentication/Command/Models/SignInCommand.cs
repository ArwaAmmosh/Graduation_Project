using Graduation_Project.Results;

namespace Graduation_Project.Features.Authentication.Command.Models
{
    public class SignInCommand : IRequest<Response<string>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    
}
