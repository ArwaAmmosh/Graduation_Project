using Graduation_Project.Entities.Identity;
using Graduation_Project.Features.Users.commands.Models;

namespace Graduation_Project.Mapping.Users
{
    public partial class UserProfile

    {
        public void AddUserMapping()
        {
            CreateMap<AddUserCommand, User>();

        }
    }
}
