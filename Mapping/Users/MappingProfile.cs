namespace Graduation_Project.Mapping.Users
{
    public partial class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AddUserMapping();
            GetUserPaginationMapping();
            GetUserByIdMapping();
            UpdateUserMapping();
        }
    }
}
