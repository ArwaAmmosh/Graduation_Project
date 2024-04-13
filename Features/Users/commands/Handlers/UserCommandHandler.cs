using AutoMapper;
using Graduation_Project.Bases;
using Graduation_Project.Entities.Identity;
using Graduation_Project.Features.Users.commands.Models;
using Graduation_Project.Resource;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SharpDX.DXGI;

namespace Graduation_Project.Features.Users.commands.Handlers

{
    public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Bases.Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _sharedResource;
        private readonly UserManager<User> _userManager;

        #endregion
        #region Constructor
        public UserCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IMapper mapper, UserManager<User> userManager) : base(stringLocalizer)
        {

            _sharedResource = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
        }


        #endregion
        #region Handle Functions
        public async Task<Bases.Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest<string>(_sharedResource[SharedResourcesKeys.EmailIsExist]);
            }
            var identityUser = _mapper.Map<User>(request);
            var creatResult = await _userManager.CreateAsync(identityUser, request.Password);
            if (!creatResult.Succeeded)
            {
                return BadRequest<string>(_sharedResource[SharedResourcesKeys.FailedToAddUser]);
            }
            return Created("");
            #endregion
        }
    }
}
