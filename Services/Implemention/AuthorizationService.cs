using Graduation_Project.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Graduation_Project.Services.Implemention
{
    public class AuthorizationService:IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly UNITOOLDbContext _dbContext;
        #endregion
        #region constructor
        public AuthorizationService(RoleManager<Role> roleManager,
                                 UserManager<User> userManager,
                                 UNITOOLDbContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }


        #endregion
        #region Handle Functions
        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole = new Role();
            identityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
                return "Success";
            return "Failed";
        }

        public async Task<string> DeleteRoleAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return "NotFound";
            //Chech if user has this role or not
            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            //return exception 
            if (users != null && users.Count() > 0) return "Used";
            //delete
            var result = await _roleManager.DeleteAsync(role);
            //success
            if (result.Succeeded) return "Success";
            //problem
            var errors = string.Join("-", result.Errors);
            return errors;
        }
        public async Task<bool> IsRoleExistById(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return false;
            else return true;
        }
        public async Task<string> EditRoleAsync(EditRoleRequest request)
        {
            //check role is exist or not

            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            if (role == null)
                return "notFound";
            role.Name = request.RoleName;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded) return "Success";
            var errors = string.Join("-", result.Errors);
            return errors;
        }

        public async Task<bool> IsRoleExistByName(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
        public async Task<List<Role>> GetRolesList()
        {
            return await _roleManager.Roles.ToListAsync();
        }
        public async Task<Role> GetRoleById(int id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }

        public async Task<MangeUserRolesResult> ManageUserRolesData(User user)
        {
            var rolesList=new List<UserRoles>();
            var response=new MangeUserRolesResult();
            var roles = await _roleManager.Roles.ToListAsync();
            response.UserId = user.Id;
            foreach(var role in roles)
            {
                var userRole = new UserRoles()
                {
                    Id= role.Id,
                    Name=role.Name
                };
                if (await _userManager.IsInRoleAsync(user,role.Name))
                {
                    userRole.HasRole= true;
                }
                else
                {
                    userRole.HasRole= false;
                }
                rolesList.Add(userRole);
            }
            response.userRoles = rolesList;
            return response;
        }

        public async Task<string> UpdateUserRoles(UpdateUserRolesRequest request)
        {
            var transact=await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                {
                    return "UserIsNull";
                }
                var userRoles = await _userManager.GetRolesAsync(user);
                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeResult.Succeeded)
                {
                    return "FailedToRemoveOldRoles";
                }
                var selectedRoles = request.userRoles.Where(x => x.HasRole == true).Select(x => x.Name);
                var addRolesResult = await _userManager.AddToRolesAsync(user, selectedRoles);
                if (!addRolesResult.Succeeded)
                {
                    return "FailedToAddNewRoles";
                }
                transact.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateUserRoles";
            }

        }
        public async Task<MangeUserClaimsResult> ManageUserClaimsData(User user)
        {
           var response=new MangeUserClaimsResult();
            var userClaimsList=new List<UserClaims>();
           response.UserId=user.Id;
           var userClaims=await _userManager.GetClaimsAsync(user);
            foreach(var claim in ClaimsStore.claims)
            {
                var userClaim = new UserClaims()
                {
                    Type= claim.Type,
                };
                if (userClaims.Any(x=>x.Type==claim.Type))
                {
                    userClaim.Value = true;
                }
                else
                {
                    userClaim.Value = false;
                }
                userClaimsList.Add(userClaim);
            }
            response.userClaims = userClaimsList;
            return response;

        }

        public async Task<string> UpdateUserClaims(UpdateUserClaimsRequest request)
        {
            var transact=await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                {
                    return "UserIsNull";
                }
                var userClaims = await _userManager.GetClaimsAsync(user);
                var removeResult = await _userManager.RemoveClaimsAsync(user, userClaims);
                if (!removeResult.Succeeded)
                {
                    return "FailedToRemoveOldClaims";
                }
                var selectedClaims = request.userClaims.Where(x => x.Value == true).Select(x => new Claim(x.Type,x.Value.ToString()));
                var addClaimsResult = await _userManager.AddClaimsAsync(user, selectedClaims);
                if (!addClaimsResult.Succeeded)
                {
                    return "FailedToAddNewClaims";
                }
                await transact.CommitAsync();;
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateUserClaims";

            }
        }


        #endregion
    }
}
