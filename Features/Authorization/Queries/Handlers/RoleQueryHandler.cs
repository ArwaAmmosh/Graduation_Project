
using Graduation_Project.Entities.Identity;
using Graduation_Project.Features.Authorization.Queries.Models;
using Graduation_Project.Features.Authorization.Queries.Results;

namespace Graduation_Project.Features.Authorization.Queries.Handlers
{
    public class RoleQueryHandler:ResponseHandler,
                                  IRequestHandler<GetRolesListQuery,Response<List<GetRolesListResult>>>,
                                  IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResult>>,
                                 IRequestHandler<MangeUserRolesQuery,Response<MangeUserRolesResult>>


    {

        #region Fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion
        #region constructor
        public RoleQueryHandler(IStringLocalizer<SharedResource> stringLocalizer, IAuthorizationService authorizationService, IMapper mapper, UserManager<User> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            _mapper = mapper;
            _userManager = userManager;
        }


        #endregion
        #region Handle Functions
        public async Task<Response<List<GetRolesListResult>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.GetRolesList();
            var result=_mapper.Map<List<GetRolesListResult>>(roles);
            return Success(result);

        }

        public async Task<Response<GetRoleByIdResult>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.GetRoleById(request.Id);
            if(role == null)
            {
                return NotFound<GetRoleByIdResult>(_stringLocalizer[SharedResourcesKeys.RoleNotExist]);
            }
            
            var result = _mapper.Map<GetRoleByIdResult>(role);
            return Success(result);
        }

        public async Task<Response<MangeUserRolesResult>> Handle(MangeUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if(user== null)
            {
                return NotFound<MangeUserRolesResult>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
            }
            var result =await _authorizationService.ManageUserRolesData(user);
            return Success(result);

        }



        #endregion
    }
    }
