using Graduation_Project.Entities.Identity;

namespace Graduation_Project.Services.Implemention
{
    public class UserServices : IUserService
    {
        #region Fields
        private readonly IFileService _fileService;
        private readonly UNITOOLDbContext uNITOOLDbContext;
        #endregion
        #region Constructor
        public UserServices(IFileService fileService)
        {
            _fileService = fileService;

        }
        #endregion
        #region Functions
        public async Task SaveChangesAsync()
        {
            await uNITOOLDbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(User entity)
        {
            uNITOOLDbContext.Set<User>().Update(entity);
            await uNITOOLDbContext.SaveChangesAsync();

        }
        public async Task<string> UploadFrontIdImage( IFormFile file)
        {
            var imageUrl = await _fileService.UploadImage("UserFrontIdImage", file);
            switch (imageUrl)
            {
                case "No Image Uploaded":
                    return "No Image Uploaded";
                    break;
                case "Failed To Upload":
                    return "Failed To Upload";
                    break;
                
            }
            return imageUrl;
        }
        public async Task<string> UploadBackIdImage(IFormFile file)
        {
            var imageUrl = await _fileService.UploadImage("UserBackIdImage", file);
            switch (imageUrl)
            {
                case "No Image Uploaded":
                    return "No Image Uploaded";
                    break;
                case "Failed To Upload":
                    return "Failed To Upload";
                    break;

            }
            return imageUrl;

        }
        public async Task<string> UploadCollegeCardFrontImage(IFormFile file)
        {
            var imageUrl = await _fileService.UploadImage("UserCollegeCardFrontImage", file);
            switch (imageUrl)
            {
                case "No Image Uploaded":
                    return "No Image Uploaded";
                    break;
                case "Failed To Upload":
                    return "Failed To Upload";
                    break;

            }
            return imageUrl;
        }
        public async Task<string> UploadCollegeCardBackImage( IFormFile file)
        {
            var imageUrl = await _fileService.UploadImage("UserdBackImage", file);
            switch (imageUrl)
            {
                case "No Image Uploaded":
                    return "No Image Uploaded";
                    break;
                case "Failed To Upload":
                    return "Failed To Upload";
                    break;

            }
            return imageUrl;
        }
        public async Task<string> UploadPersonalImage(IFormFile file)
        {
            var imageUrl = await _fileService.UploadImage("UserPersonalImage", file);
            switch (imageUrl)
            {
                case "No Image Uploaded":
                    return "No Image Uploaded";
                    break;
                case "Failed To Upload":
                    return "Failed To Upload";
                    break;

            }
            return imageUrl;
        }
        #endregion

    }
}
