namespace Graduation_Project.Mapping.Users
{
    public partial class MappingProfile
    {
        public void GetUserPaginationMapping()
        {
            CreateMap<User, GetUserPaginationResponse>();
        }
    }
}
