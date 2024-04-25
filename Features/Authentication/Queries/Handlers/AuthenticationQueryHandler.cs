using Graduation_Project.Features.Authentication.Queries.Models;
using Graduation_Project.Resource;
using Microsoft.Extensions.Localization;
using SharpDX.DXGI;

namespace Graduation_Project.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler : ResponseHandler,
                                              IRequestHandler<AuthorizeUserQuery, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IAuthenticationService _authenticationService;

        #endregion

        #region Constructors
        public AuthenticationQueryHandler(IStringLocalizer<SharedResource> stringLocalizer,
                                            IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authenticationService = authenticationService;
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
        #endregion
    }
}
