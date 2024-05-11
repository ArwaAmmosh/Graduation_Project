using Graduation_Project.Services.Abstracts;
using System.Linq.Expressions;
using System.IO;
namespace Graduation_Project.Services.Implemention
{
    public class FileService: IFileService
    {
        #region Fields
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _host = "https://localhost:7257/";

        #endregion
        #region Constructor
        public FileService(IWebHostEnvironment webHostEnvironment) 
        {
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion
        #region Functions
        public async Task<string> UploadImage(string Location, IFormFile file)
        {
            var path=_webHostEnvironment.WebRootPath + "/" + Location+"/";
            var extention=Path.GetExtension(file.FileName);
            var filename=Guid.NewGuid().ToString().Replace("-",string.Empty)+extention;
            if (file.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = File.Create(path + filename))
                    {
                        await file.CopyToAsync(fileStream);
                        await fileStream.FlushAsync();
                        return $"{_host}{Location}/{filename}";
                    }

                }
                catch (Exception)
                {
                    return "Failed To Upload";
                }
            }
            else
            {
                return "No Image Uploaded";
            }
        }


    }



}

    #endregion


