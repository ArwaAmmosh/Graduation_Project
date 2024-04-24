using Graduation_Project.Entities.Identity;
using Graduation_Project.Features.Users.Queries.Results;

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
