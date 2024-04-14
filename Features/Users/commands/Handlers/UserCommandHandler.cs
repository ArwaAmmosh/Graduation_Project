
namespace Graduation_Project.Features.Users.commands.Handlers

{
    public class UserCommandHandler : ResponseHandler,
                                      IRequestHandler<AddUserCommand, Response<string>>,
                                      IRequestHandler<UpdateUserCommand, Response<string>>
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
            return Created("");
            #endregion
        }
        public async Task<Bases.Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var oldUser = _userManager.FindByIdAsync(request.Id.ToString());
            if (oldUser == null)
            {
                return NotFound<string>();
            }
            var newUser = await _mapper.Map(request, oldUser);
            newUser.UserName = newUser.FirstName + newUser.LastName;
            var result = await _userManager.UpdateAsync(newUser);
            if (!result.Succeeded)
            {
                return BadRequest<string>(result.Errors.FirstOrDefault().Description);
            }
            return Success("");

        }
    }
}
