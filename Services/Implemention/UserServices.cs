using Graduation_Project.Entities.Identity;
using Graduation_Project.Services.Abstracts;

namespace Graduation_Project.Services.Implemention
{
    public class UserServices : IUserService
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

        public UserServices(UserManager<User> userManager,
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
        #region Functions
        public async Task SaveChangesAsync()
        {
            await _uNITOOLDbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(User entity)
        {
            _uNITOOLDbContext.Set<User>().Update(entity);
            await _uNITOOLDbContext.SaveChangesAsync();

        }
        public async Task<string> UploadFrontIdImage(IFormFile file)
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
        public async Task<string> UploadCollegeCardBackImage(IFormFile file)
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

        public async Task<string> CreateAsync(User user, string Password)
        {

            var trans = await _uNITOOLDbContext.Database.BeginTransactionAsync();
            try
            {
                //if Email is Exist
                var existUser = await _userManager.FindByEmailAsync(user.Email);
                //email is Exist
                if (existUser != null) return "EmailIsExist";

                //if username is Exist
                user.UserName = user.FirstName + user.LastName;
                var userByUserName = await _userManager.FindByNameAsync(user.UserName);
                //username is Exist
                if (userByUserName != null) return "UserNameIsExist";
                //Create
                var createResult = await _userManager.CreateAsync(user, Password);
                //Failed
                if (!createResult.Succeeded)
                    return string.Join(",", createResult.Errors.Select(x => x.Description).ToList());

                await _userManager.AddToRoleAsync(user, "ViewUser");
                //Send Confirm Email
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
               // var resquestAccessor = _httpContextAccessor.HttpContext.Request;
               // var returnUrl = resquestAccessor.Scheme + "://" + resquestAccessor.Host + _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code });
               // var message = $"To Confirm Email Click Link: <a href='{returnUrl}'>Link Of Confirmation</a>";
                //$"/Api/V1/Authentication/ConfirmEmail?userId={user.Id}&code={code}";
                
                    var emailTitle = "Welcome to UNITOOL - Confirm Your Email";
                    var emailBody = $@"
                                   <html lang=""en"">
                                        <head>
                                             <meta charset=""UTF-8"">
                                             <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                             <title>{emailTitle}</title>
                                         </head>
                                         <body style=""font-family: 'Arial', sans-serif; background-color: #f4f4f4; color: #333; padding: 20px; text-align: center;"">
                                               <div style=""background-color: #fff; border-radius: 10px; padding: 20px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);"">
                                                   <h2 style=""color: #007bff;"">Welcome to UNITOOL! 🎉</h2>
                                                   <p>Thank you for joining UNITOOL, your Exchanging Tools Platform. We're thrilled to welcome you to our platform!</p>
                                                   <p style=""background-color: #007bff; color: #fff; padding: 5px; border-radius: 5px;"">Your verification code is: <stron>{code}</strong></p>
                                                   <p>This simple step ensures the security of your account and unlocks a world of convenience for managing your medications.</p>
                                                   <p>If you haven't signed up for UNITOOL, please disregard this email.</p>
                                                   <p>Get ready to trade and exchange Tools!</p>
                                                   <p>Best regards,<br>UNITOOL Team</p>
                                               </div>
                                               <div style=""margin-top: 20px; font-size: 12px; color: #888;"">
                                                   <p>This is an automated message. Please do not reply to this email.</p>
                                                   <p>&copy; {DateTime.Now.Year} UNITOOL. All rights reserved.</p>
                                               </div>
                                           </body>
                                           </html> ";
                                     



                    //message or body
                    await _emailsService.SendEmail(user.Email, emailBody, "ConFirm Email");

                    await trans.CommitAsync();
                    return "Success";
                }
                catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }
        #endregion

    }
}
