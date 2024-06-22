

namespace Graduation_Project.Features.Users.commands.Handlers

{
    public class UserCommandHandler : ResponseHandler,
                                      IRequestHandler<AddUserCommand, Response<JwtAuthResult>>, 
                                      IRequestHandler<UpdateUserCommand, Response<string>>,
                                      IRequestHandler<DeleteUserCommand, Response<string>>,
                                      IRequestHandler<ChangeUserPasswordCommand, Response<string>>



    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _sharedResource;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuthenticationService _authenticationService;
        
        #endregion
        #region Constructor
        public UserCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IMapper mapper, UserManager<User> userManager,IUserService userService,ICurrentUserService currentUserService, IAuthenticationService authenticationService) : base(stringLocalizer)
        {

            _sharedResource = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
            _userService = userService;
            _currentUserService = currentUserService;
            _authenticationService = authenticationService;
            
        }


        #endregion
        #region Handle Functions
        public async Task<Response<JwtAuthResult>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var identityUser = _mapper.Map<User>(request);
            //Create
            var createResult = await _userService.CreateAsync(identityUser, request.Password);
            switch (createResult)
            {
                case "EmailIsExist": return BadRequest<JwtAuthResult>(_sharedResource[SharedResourcesKeys.EmailIsExist]);
                case "UserNameIsExist": return BadRequest<JwtAuthResult>(_sharedResource[SharedResourcesKeys.UserNameIsExist]);
                case "ErrorInCreateUser": return BadRequest<JwtAuthResult>(_sharedResource[SharedResourcesKeys.FaildToAddUser]);
                case "Failed": return BadRequest<JwtAuthResult>(_sharedResource[SharedResourcesKeys.TryToRegisterAgain]);
                case "Success":
                    var result = await _authenticationService.GetJWTToken(identityUser);
                    return Success<JwtAuthResult>(result);
                default: return BadRequest<JwtAuthResult>(createResult);
            }
        }

    

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            //check if user is exist
            var oldUser = await _userManager.FindByIdAsync(_currentUserService.GetUserId().ToString());
            //if Not Exist notfound
            if (oldUser == null) return NotFound<string>();
            //mapping
            var newUser = _mapper.Map(request,oldUser);

            //if username is Exist
            var userByUserName = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == newUser.UserName && x.Id != newUser.Id);
            //username is Exist
            if (userByUserName != null)
            {
                return BadRequest<string>(_sharedResource[SharedResourcesKeys.UserNameIsExist]);
            }
            newUser.FrontIdImage = await _userService.UploadUserImages(request.FrontIdImage);
            newUser.BackIdImage = await _userService.UploadUserImages(request.BackIdImage);
            newUser.CollegeCardBackImage = await _userService.UploadUserImages(request.CollegeCardBackImage);
            newUser.CollegeCardFrontImage = await _userService.UploadUserImages(request.CollegeCardFrontImage);
            newUser.PersonalImage = await _userService.UploadPersonalImage(request.PersonalImage);
            newUser.IsComplete = true;

            await _userManager.RemoveFromRoleAsync(newUser, "ViewUser");
            await _userManager.AddToRoleAsync(newUser, "AdmittedUser");
            //update
            var result = await _userManager.UpdateAsync(newUser);
            //result is not success
            if (!result.Succeeded) return BadRequest<string>(_sharedResource[SharedResourcesKeys.UpdateFailed]);
            //message
            return Success((string)_sharedResource[SharedResourcesKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user=await _userManager.FindByIdAsync(_currentUserService.GetUserId().ToString());
            if (user == null)
            {
                return NotFound<string>();
            }
            var result= await _userManager.DeleteAsync(user);
            if(!result.Succeeded)
            { 
                return BadRequest<string>(_sharedResource[SharedResourcesKeys.DeletedFailed]);
            }
            return Success<string>( _sharedResource[SharedResourcesKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(_currentUserService.GetUserId().ToString());
            if (user == null)
            {
                return NotFound<string>();
            }
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest<string>(_sharedResource[SharedResourcesKeys.ChangePassFailed]);
            }
            return Success<string>(_sharedResource[SharedResourcesKeys.Success]);
        }

        #endregion

    }
}
