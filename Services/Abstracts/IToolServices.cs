namespace Graduation_Project.Services.Abstracts
{
    public interface IToolServices
    {
        public Task<string> Upload1Image(IFormFile file);
        public Task<string> Upload2Image(IFormFile file);
        public Task<string> Upload3Image(IFormFile file);
        public Task<string> Upload4Image(IFormFile file);
    }
}
