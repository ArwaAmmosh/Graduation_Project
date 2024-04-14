using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace Graduation_Project.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler:ResponseHandler,IRequestHandler<SignInCommand,Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signManger;
        private readonly IAuthenticationService _authenticationService;
        #endregion
        #region Constructor
        public AuthenticationCommandHandler(IStringLocalizer<SharedResource> stringLocalizer,UserManager<User> userManager,SignInManager<User> signInManager,IAuthenticationService authenticationService) : base(stringLocalizer) 
        { 
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _signManger = signInManager;
            _authenticationService = authenticationService;

        }



        #endregion
        #region Handle function
        public async Task<Response<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNameNotExist]);
            }
            var signInResult = _signManger.CheckPasswordSignInAsync(user, request.Password,false);
            if (!signInResult.IsCompletedSuccessfully)
            {
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.PasswordNotCorrect]);

            }
            var acessToken =await _authenticationService.GetJWTToken(user);
            return Success(acessToken,"SignIn Sucessfully");
        }
        #endregion
    }
}
