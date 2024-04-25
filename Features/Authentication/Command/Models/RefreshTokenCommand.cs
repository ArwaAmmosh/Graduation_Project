namespace Graduation_Project.Features.Authentication.Command.Models
{
    public class RefreshTokenCommand:IRequest<Response<JwtAuthResult>>
    {
        public string AccessToken { set; get; }
        public string RefreshToken { set; get; }


    }
}
