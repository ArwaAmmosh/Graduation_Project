namespace Graduation_Project.Mapping.Roles
{
    public partial class RoleProfile
    {
        public void GetRolesListMapping()
        {
            CreateMap<Role, GetRolesListResult>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        }
    }
}
