namespace Graduation_Project.Mapping.Users
{
    public partial class MappingProfile
    {
        public void GetUserByIdMapping()
        {
            CreateMap<User, GetUserByIdResponse>();
        }
    }
}
