using Graduation_Project.Entities.Identity;
using Graduation_Project.Infrastructure;

namespace Graduation_Project.Services.Abstracts
{
    public interface IUserService
    {
        public Task<string> UploadUserImages( IFormFile file);
        public  Task<string> UploadPersonalImage( IFormFile file);
        Task SaveChangesAsync();
        Task UpdateAsync(User entity);
        public Task<string> CreateAsync(User entity,string Password);
    }
}
