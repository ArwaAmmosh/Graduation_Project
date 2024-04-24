

using Graduation_Project.Entities.Identity;
using Graduation_Project.Results;
using System.IdentityModel.Tokens.Jwt;

namespace Graduation_Project.Services.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<string> GetJWTToken(User user);
    }
    
}
