namespace Graduation_Project.Mapping.Users
{
    public partial class MappingProfile
    {
        public void UpdateUserMapping()
        {
            CreateMap<UpdateUserCommand, User>();
        }
    }
}
