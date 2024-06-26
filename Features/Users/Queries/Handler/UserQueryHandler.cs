using Graduation_Project.Bases;
using Graduation_Project.Entities.Identity;
using Graduation_Project.Features.Users.Queries.Models;
using Graduation_Project.Features.Users.Queries.Results;
using Graduation_Project.Resource;
using Graduation_Project.Wrapper;
using Microsoft.Extensions.Localization;
using SharpDX.DXGI;

namespace Graduation_Project.Features.Users.Queries.Handler
{
    public class UserQueryHandler : ResponseHandler,
          IRequestHandler<GetUserPaginationQuery, PaginatedResult<GetUserPaginationReponse>>,
          IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _sharedResources;
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly IFileService _fileService;


        #endregion

        #region Constructors
        public UserQueryHandler(IStringLocalizer<SharedResource> stringLocalizer,
                                  IMapper mapper,
                                  UserManager<User> userManager,
                                  ICurrentUserService currentUserService,
                                  IFileService fileService) : base(stringLocalizer)
        {
            _mapper = mapper;
            _sharedResources = stringLocalizer;
            _userManager = userManager;
            _currentUserService = currentUserService;
            _fileService = fileService;
        }
        #endregion

        #region Handle Functions
        public async Task<PaginatedResult<GetUserPaginationReponse>> Handle(GetUserPaginationQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            var paginatedList = await _mapper.ProjectTo<GetUserPaginationReponse>(users)
                                            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id==request.Id);
            //var user = await _userManager.FindByIdAsync(_currentUserService.GetUserId().ToString());
            if (user == null) return NotFound<GetUserByIdResponse>(_sharedResources[SharedResourcesKeys.NotFound]);
            var result = _mapper.Map<GetUserByIdResponse>(user);
            return Success(result);
        }
        #endregion
    }

    }

