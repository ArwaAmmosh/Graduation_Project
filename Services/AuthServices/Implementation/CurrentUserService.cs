
namespace Graduation_Project.Services.AuthServices.Implementation
{
    public class CurrentUserService: ICurrentUserService
    {
        #region Fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationService _AuthenticationService;
        #endregion
        #region Constructors
        public CurrentUserService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, IAuthenticationService AuthenticationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _AuthenticationService = AuthenticationService;
        }
        #endregion
        #region Functions
        public int GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == nameof(UserClaimModel.Id)).Value;
            if (userId == null)
            {
                throw new UnauthorizedAccessException();
            }
            return int.Parse(userId);
        }

        public async Task<User> GetUserAsync()
        {
            var userId = GetUserId();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new UnauthorizedAccessException(); 
            }
            return user;
        }

        public async Task<List<string>> GetCurrentUserRolesAsync()
        {
            var user = await GetUserAsync();
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
        public async Task<UserDto?> GetCurrentAuthenticatedUserAsync()
        {
            var userIdClaim = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return null;

            var userId = userIdClaim.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return null;

            return new UserDto
            {
                Email = user.Email!,
                DisplayName = user.UserName!,
                Token = (await _AuthenticationService.GetJWTToken(user))?.AccessToken
            };
        }
        #endregion
    }
}