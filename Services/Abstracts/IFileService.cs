namespace Graduation_Project.Services.Abstracts
{
    public interface IFileService
    {
        public Task<string> UploadImage(string Location, IFormFile file);


    }
}
