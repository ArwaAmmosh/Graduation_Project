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
    public class UserCommandHandler : ResponseHandler,
                                      IRequestHandler<AddUserCommand, Bases.Response<string>>, 
                                      IRequestHandler<UpdateUserCommand, Response<string>>,
                                      IRequestHandler<DeleteUserCommand, Response<string>>,
                                      IRequestHandler<ChangeUserPasswordCommand, Response<string>>



    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _sharedResource;
        private readonly UserManager<User> _userManager;
        private readonly IUserService userService;

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
            if (user != null)
            {
                return BadRequest<string>(_sharedResource[SharedResourcesKeys.EmailIsExist]);
            }
            var identityUser = _mapper.Map<User>(request);
            identityUser.UserName = identityUser.FirstName + identityUser.LastName;
            var creatResult = await _userManager.CreateAsync(identityUser, request.Password);
            if (!creatResult.Succeeded)
            {
                return BadRequest<string>(creatResult.Errors.FirstOrDefault().Description);
            }
            
                await _userManager.AddToRoleAsync(identityUser, "User");
            
            return Created("");
        }

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            //check if user is exist
            var oldUser = await _userManager.FindByIdAsync(request.Id.ToString());
            //if Not Exist notfound
            if (oldUser == null) return NotFound<string>();
            //mapping
            var newUser = _mapper.Map(request,oldUser);

            //if username is Exist
            var userByUserName = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == newUser.UserName && x.Id != newUser.Id);
            //username is Exist
            if (userByUserName != null)
            {
                return BadRequest<string>(_sharedResource[SharedResourcesKeys.UserNameIsExist]);
            }
            newUser.FrontIdImage = await userService.UploadFrontIdImage(request.FrontIdImage);
            newUser.BackIdImage = await userService.UploadBackIdImage(request.BackIdImage);
            newUser.CollegeCardBackImage = await userService.UploadCollegeCardBackImage(request.CollegeCardBackImage);
            newUser.CollegeCardFrontImage = await userService.UploadCollegeCardFrontImage(request.CollegeCardFrontImage);
            newUser.PersonalImage = await userService.UploadPersonalImage(request.PersonalImage);


            //update
            var result = await _userManager.UpdateAsync(newUser);
            //result is not success
            if (!result.Succeeded) return BadRequest<string>(_sharedResource[SharedResourcesKeys.UpdateFailed]);
            //message
            return Success((string)_sharedResource[SharedResourcesKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user=await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                return NotFound<string>();
            }
            var result= await _userManager.DeleteAsync(user);
            if(!result.Succeeded)
            { 
                return BadRequest<string>(_sharedResource[SharedResourcesKeys.DeletedFailed]);
            }
            return Success<string>( _sharedResource[SharedResourcesKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                return NotFound<string>();
            }
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest<string>(_sharedResource[SharedResourcesKeys.ChangePassFailed]);
            }
            return Success<string>(_sharedResource[SharedResourcesKeys.Success]);
        }

        #endregion

    }
}
