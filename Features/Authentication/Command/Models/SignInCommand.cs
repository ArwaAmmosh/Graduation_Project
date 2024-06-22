
namespace Graduation_Project.Features.Authentication.Command.Models
{
    public class SignInCommand : IRequest<Response<LoginResponseDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    
}
