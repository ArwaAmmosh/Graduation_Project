namespace Graduation_Project.Services.Implemention
{
    public class ToolServices : IToolServices
    {
        #region Fields
        private readonly IFileService _fileService;
        private readonly UNITOOLDbContext _uNITOOLDbContext;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailsService _emailsService;
        private readonly IUrlHelper _urlHelper;
        #endregion
        #region Constructor

        public ToolServices(UserManager<User> userManager,
                              IHttpContextAccessor httpContextAccessor,
                              IEmailsService emailsService,
                              UNITOOLDbContext applicationDBContext,
                              IUrlHelper urlHelper,
                              IFileService fileService
            )
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _emailsService = emailsService;
            _uNITOOLDbContext = applicationDBContext;
            _urlHelper = urlHelper;
            _fileService = fileService;
        }

        #endregion
        public async Task<string> Upload1Image(IFormFile file)
        {
            var imageUrl =await  _fileService.UploadImage("ToolImages1", file);
            switch (imageUrl)
            {
                case "No Image Uploaded":
                    return "No Image Uploaded";
                    break;
                case "Failed To Upload":
                    return "Failed To Upload";
                    break;

            }
            return  imageUrl;
        }

        public async Task<string> Upload2Image(IFormFile file)
        {
            var imageUrl = await _fileService.UploadImage("ToolImages2", file);
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

        public async Task<string> Upload3Image(IFormFile file)
        {
            var imageUrl = await _fileService.UploadImage("ToolImages3", file);
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

        public async Task<string> Upload4Image(IFormFile file)
        {
            var imageUrl = await _fileService.UploadImage("ToolImages4", file);
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
    }
}
