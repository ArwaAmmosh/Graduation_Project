namespace Graduation_Project.Features.Authorization.Queries.Handlers
{
    public class ClaimsQueryHandler: ResponseHandler,
                                  IRequestHandler<MangeUserClaimsQuery, Response<MangeUserClaimsResult>>

    {

        #region Fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<User> _userManager;
        #endregion
        #region constructor
        public ClaimsQueryHandler(IStringLocalizer<SharedResource> stringLocalizer,UserManager<User> userManager,IAuthorizationService authorizationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }


        #endregion
        #region Handle Functions
   
        public async Task<Response<MangeUserClaimsResult>> Handle(MangeUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                return NotFound<MangeUserClaimsResult>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
            }
            var result = await _authorizationService.ManageUserClaimsData(user);
            return Success(result);
        }



        #endregion
    }
}
