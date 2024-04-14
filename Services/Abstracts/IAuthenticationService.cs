namespace Graduation_Project.Services.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<string> GetJWTToken(User user); 
    }
}
