namespace Graduation_Project.Features.Authorization.Commands.Handler
{
    public class RoleCommandHandler : ResponseHandler,
                                      IRequestHandler<AddRoleCommand, Response<string>>,
                                      IRequestHandler<EditRoleCommand, Response<string>>,
                                      IRequestHandler<DeleteRoleCommand, Response<string>>,
                                      IRequestHandler<UpdateUserRolesCommand, Response<string>>





    {
        #region Fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion
        #region constructor
        public RoleCommandHandler(IStringLocalizer<SharedResource> stringLocalizer,IAuthorizationService authorizationService) :base(stringLocalizer) 
        {
            _stringLocalizer = stringLocalizer;     
            _authorizationService = authorizationService;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var response = await _authorizationService.AddRoleAsync(request.RoleName);
            if (response == "Success")
                return Success(response);
            else
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.AddFailed]);

        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.EditRoleAsync(request);
            if (result == "notFound")
                return NotFound<string>();
            else if (result == "Success")
                return Success(result);
            else
                return BadRequest<string>(result);
         }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.DeleteRoleAsync(request.Id);
            if (result == "NotFound") return NotFound<string>();
            else if (result == "Used") return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.RoleIsUsed]);
            else if (result == "Success") return Success((string)_stringLocalizer[SharedResourcesKeys.Deleted]);
            else
                return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserRoles(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
                case "FailedToRemoveOldRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToRemoveOldRoles]);
                case "FailedToAddNewRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewRoles]);
                case "FailedToUpdateUserRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateUserRoles]);
            }
            return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
        }
        #endregion

    }
}
