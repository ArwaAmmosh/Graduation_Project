using Graduation_Project.Features.Users.Queries.Results;

namespace Graduation_Project.Helpers
{
    public class JwtAuthResult
    {
        public string AccessToken { get; set; }
        public GetUserByIdResponse Getuser { get; set; }
        //public RefreshToken refreshToken { get; set; }
    }
    public class RefreshToken
    {
        public string UserName { get; set; }
        public string TokenString { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
