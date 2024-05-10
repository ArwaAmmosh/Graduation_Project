using Graduation_Project.Features.Users.Queries.Results;

namespace Graduation_Project.Features.GuestMode.Queries.Handler
{
    public class GuestModeQueryHandler: ResponseHandler,
          IRequestHandler<GetGuestByIdQuery, Response<GetGuestByIdResponse>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _sharedResources;
        private readonly UserManager<GuestModeUser> _userManager;

        #endregion

        #region Constructors
        public GuestModeQueryHandler(IStringLocalizer<SharedResource> stringLocalizer,
                                  IMapper mapper,
                                  UserManager<GuestModeUser> userManager) : base(stringLocalizer)
        {
            _mapper = mapper;
            _sharedResources = stringLocalizer;
            _userManager = userManager;
        }
        #endregion

        #region Handle Functions

        public async Task<Response<GetGuestByIdResponse>> Handle(GetGuestByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return NotFound<GetGuestByIdResponse>(_sharedResources[SharedResourcesKeys.NotFound]);
            var result = _mapper.Map<GetGuestByIdResponse>(user);
            return Success(result);
        }
        #endregion
    }

}

