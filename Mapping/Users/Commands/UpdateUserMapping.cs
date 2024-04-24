using Graduation_Project.Entities.Identity;
using Graduation_Project.Features.Users.commands.Models;

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
