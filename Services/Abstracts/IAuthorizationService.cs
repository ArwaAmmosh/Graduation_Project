namespace Graduation_Project.Services.Abstracts
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExistByName(string roleName);
        public Task<string> EditRoleAsync(EditRoleRequest request);
        public Task<string> DeleteRoleAsync(int roleId);
        public Task<bool> IsRoleExistById(int roleId);
        public Task<List<Role>> GetRolesList();
        public Task<Role> GetRoleById(int id);
        public Task<MangeUserRolesResult> ManageUserRolesData(User user);
        public Task<string> UpdateUserRoles(UpdateUserRolesRequest request);
        public Task<MangeUserClaimsResult> ManageUserClaimsData(User user);
        public Task<string> UpdateUserClaims(UpdateUserClaimsRequest request);







    }
}
