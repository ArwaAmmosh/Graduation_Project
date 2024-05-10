namespace Graduation_Project.Mapping.GuestModeMapping
{
    public partial class GuestModeMapping
    {
        public void AddGuestInfoMapping()
        {
            CreateMap<AddNewGuestInfoCommand, GuestModeUser>();
        }
    }
}
