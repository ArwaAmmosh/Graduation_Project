namespace Graduation_Project.Features.Authorization.Commands.Handler
{
    public class ClaimsCommandHandler:ResponseHandler,
                                      IRequestHandler<UpdateUserClaimsCommand,Response<string>>
                                      
    {
        #region Fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion
        #region constructor
        public ClaimsCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IAuthorizationService authorizationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
        }


        #endregion
        #region Handle Functions

        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserClaims(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
                case "FailedToRemoveOldClaims": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToRemoveOldClaims]);
                case "FailedToAddNewClaims": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewClaims]);
                case "FailedToUpdateUserClaims": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateClaims]);
            }
            return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
        }
        #endregion

    }
}
