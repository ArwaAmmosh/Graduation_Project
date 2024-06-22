namespace Graduation_Project.Mapping.Roles
{
    public partial class RoleProfile
    {
        public void GetRoleByIdMapping()
        {
            CreateMap<Role, GetUserByIdResult>();
        }
    }
}
