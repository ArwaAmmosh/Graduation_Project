using Graduation_Project.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Features.GuestMode.Command.Handler
{
    public class GuestModeCommandHandler : ResponseHandler,
                                             IRequestHandler<AddNewGuestInfoCommand, Response<string>>,
                                             IRequestHandler<DeleteGuestInfoCommand, Response<string>>
    {

        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _sharedResource;
        private readonly UserManager<GuestModeUser> _userManager;

        #endregion
        #region Constructor
        public GuestModeCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IMapper mapper, UserManager<GuestModeUser> userManager) : base(stringLocalizer)
        {

            _sharedResource = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
        }


        #endregion
        #region Handle Functions
        public async Task<Response<string>> Handle(AddNewGuestInfoCommand request, CancellationToken cancellationToken)
        {
            var guestUser = _mapper.Map<GuestModeUser>(request);
            var result = _userManager.CreateAsync(guestUser);
            if (!result.IsCompletedSuccessfully)
                return BadRequest<string>(_sharedResource[SharedResourcesKeys.FaildToAddGuestInformation]);
            return Success<string>("");


        }
        public async Task<Response<string>> Handle(DeleteGuestInfoCommand request, CancellationToken cancellationToken)
        {
            var guestUser = await _userManager.FindByIdAsync(request.Id.ToString());
            if (guestUser == null)
            {
                return NotFound<string>();
            }
            var result = await _userManager.DeleteAsync(guestUser);
            if (!result.Succeeded)
            {
                return BadRequest<string>(_sharedResource[SharedResourcesKeys.DeletedFailed]);
            }
            return Success<string>(_sharedResource[SharedResourcesKeys.Deleted]);
        }
        #endregion
    }
}
