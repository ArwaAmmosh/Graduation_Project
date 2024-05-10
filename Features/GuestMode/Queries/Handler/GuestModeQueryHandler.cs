using Graduation_Project.Features.Users.Queries.Results;

namespace Graduation_Project.Features.GuestMode.Queries.Handler
{
    public class GuestModeQueryHandler: ResponseHandler,
                                   IRequestHandler<GetGuestByIdQuery, Response<GetGuestByIdResponse>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _sharedResources;
        private readonly UNITOOLDbContext _uNITOOLDbContext;
        #endregion

        #region Constructors
        public GuestModeQueryHandler(IStringLocalizer<SharedResource> stringLocalizer,
                                  IMapper mapper,
                                  UNITOOLDbContext uNITOOLDbContext) : base(stringLocalizer)
        {
            _mapper = mapper;
            _sharedResources = stringLocalizer;
            _uNITOOLDbContext= uNITOOLDbContext;
        }
        #endregion

        #region Handle Functions

        public async Task<Response<GetGuestByIdResponse>> Handle(GetGuestByIdQuery request, CancellationToken cancellationToken)
        {
            var user = _uNITOOLDbContext.GuestModes.FirstOrDefault(f => f.Id == request.Id);
            if (user == null) return NotFound<GetGuestByIdResponse>(_sharedResources[SharedResourcesKeys.NotFound]);
            var result = _mapper.Map<GetGuestByIdResponse>(user);
            return Success(result);
        }
        #endregion
    }

}

