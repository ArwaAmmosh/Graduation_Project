﻿using Graduation_Project.Features.Authentication.Queries.Models;
using Graduation_Project.Resource;
using Microsoft.Extensions.Localization;
using SharpDX.DXGI;

namespace Graduation_Project.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler : ResponseHandler,
                                              IRequestHandler<AuthorizeUserQuery, Response<string>>,
                                              IRequestHandler<ConfirmEmailQuery, Response<string>>,
                                              IRequestHandler<ConfirmResetPasswordQuery, Response<string>>


    {
        #region Fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICurrentUserService _currentUserService;
        #endregion

        #region Constructors
        public AuthenticationQueryHandler(IStringLocalizer<SharedResource> stringLocalizer,
                                            IAuthenticationService authenticationService,
                                            ICurrentUserService currentUserService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authenticationService = authenticationService;
            _currentUserService = currentUserService;
        }


        #endregion
        #region Handle Function
        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.AccessToken);
            if (result == "NotExpired")
                return Success(result);
            return Unauthorized<string>(_stringLocalizer[SharedResourcesKeys.TokenIsExpired]);
        }
        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var userId= _currentUserService.GetUserId();
            var confirmEmail = await _authenticationService.ConfirmEmail(userId, request.Code);
            if (confirmEmail == "ErrorWhenConfirmEmail")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.ErrorWhenConfirmEmail]);
           else if (confirmEmail == "NotCorrect")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NotCorrect]);
            return Success<string>(_stringLocalizer[SharedResourcesKeys.ConfirmEmailDone]);
        }

        public async Task<Response<string>> Handle(ConfirmResetPasswordQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ConfirmResetPassword(request.Code, request.Email);
            switch (result)
            {
                case "UserNotFound": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.InvaildCode]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.InvaildCode]);
            }
        }
        #endregion
    }
}
