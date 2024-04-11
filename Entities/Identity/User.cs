using Microsoft.AspNetCore.Identity;

namespace Graduation_Project.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; }
        public string Univserity { get; set; }
    }
}
