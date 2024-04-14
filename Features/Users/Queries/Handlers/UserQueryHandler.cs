
namespace Graduation_Project.Features.Users.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler,
                                    IRequestHandler<GetUserPaginationQuery, PaginatedResult<GetUserPaginationResponse>>,
                                    IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>

    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _sharedResource;
        private readonly UserManager<User> _userManager;
        #endregion
        #region Constructors
        public UserQueryHandler(IMapper mapper, IStringLocalizer<SharedResource> sharedResource, UserManager<User> userManager)
        {
            _mapper = mapper;
            _sharedResource = sharedResource;
            _userManager = userManager;
        }
        #endregion
        #region Handle Function
        public async Task<PaginatedResult<GetUserPaginationResponse>> Handle(GetUserPaginationQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            var paginatedList = await _mapper.ProjectTo<GetUserPaginationResponse>(users).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                return NotFound<GetUserByIdResponse>(_sharedResource[SharedResourcesKeys.NotFound]);
            }
            var result = _mapper.Map<GetUserByIdResponse>(user);
            return Success(result);
        }
        #endregion
    }
}
