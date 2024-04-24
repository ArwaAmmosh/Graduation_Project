using Graduation_Project.Entities.Identity;
using Graduation_Project.Infrastructure;

namespace Graduation_Project.Services.Abstracts
{
    public interface IUserService
    {
        public Task<string> UploadFrontIdImage( IFormFile file);
        public Task<string> UploadBackIdImage(IFormFile file);
        public  Task<string> UploadCollegeCardFrontImage( IFormFile file);
        public  Task<string> UploadCollegeCardBackImage(IFormFile file);
        public  Task<string> UploadPersonalImage( IFormFile file);
        Task SaveChangesAsync();
        Task UpdateAsync(User entity);
    }
}
