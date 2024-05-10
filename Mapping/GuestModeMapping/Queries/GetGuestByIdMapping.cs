using Graduation_Project.Features.Users.Queries.Results;

namespace Graduation_Project.Mapping.GuestModeMapping;

public partial class GuestModeMapping
{
    public void GetGuestByIdMapping()
    {
        CreateMap<GuestModeUser, GetGuestByIdResponse>();
    }

}
