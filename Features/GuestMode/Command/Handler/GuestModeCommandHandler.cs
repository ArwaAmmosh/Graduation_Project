using Graduation_Project.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace Graduation_Project.Features.GuestMode.Command.Handler
{
    public class GuestModeCommandHandler : ResponseHandler,
                                             IRequestHandler<AddNewGuestInfoCommand, Response<string>>,
                                             IRequestHandler<DeleteGuestInfoCommand, Response<string>>
    {

        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _sharedResource;
        private readonly UNITOOLDbContext _uNITOOLDbContext;
        #endregion
        #region Constructor
        public GuestModeCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IMapper mapper,UNITOOLDbContext uNITOOLDbContext) : base(stringLocalizer)
        {

            _sharedResource = stringLocalizer;
            _mapper = mapper;
            _uNITOOLDbContext= uNITOOLDbContext;
        }


        #endregion
        #region Handle Functions
        public async Task<Response<string>> Handle(AddNewGuestInfoCommand request, CancellationToken cancellationToken)
        {
            var guestUser = _mapper.Map<GuestModeUser>(request);
            var result = _uNITOOLDbContext.AddAsync(guestUser);
            if (!result.IsCompletedSuccessfully)
                return BadRequest<string>(_sharedResource[SharedResourcesKeys.FaildToAddGuestInformation]);
             _uNITOOLDbContext.SaveChanges();
            return Success<string>("");


        }
        public async Task<Response<string>> Handle(DeleteGuestInfoCommand request, CancellationToken cancellationToken)
        {
            var guestUser = await _uNITOOLDbContext.GuestModes.FindAsync(request.Id);
            if (guestUser == null)
            {
                return NotFound<string>();
            }
            var result = _uNITOOLDbContext.GuestModes.Remove(guestUser);
            await _uNITOOLDbContext.SaveChangesAsync();
            return Success<string>(_sharedResource[SharedResourcesKeys.Deleted]);

        }
        #endregion
    }
}
