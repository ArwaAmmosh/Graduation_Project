using EntityFrameworkCore.EncryptColumn.Attribute;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Graduation_Project.Entities.Identity
{
    public class User : IdentityUser<int>
    { 
        public User()
        {
            UserRefreshTokens = new HashSet<UserRefreshToken>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Univserity { get; set; }
        public string? Government { get; set; }
        public string? Code { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? AcadmicYear { get; set; }
        public string? College { get; set; }
        public string? FrontIdImage { get; set; }
        public string? BackIdImage { get; set; }
        public string? CollegeCardFrontImage { get; set; }
        public string? CollegeCardBackImage { get; set; }
        public string? PersonalImage { get; set; }
        public string? NationalId { get; set; }
        public ICollection<Tool>? Tool { get; set; }
        public ICollection<FavoriteTool>? FavoriteTool { get; set; }

        [InverseProperty(nameof(UserRefreshToken.user))]
        public virtual ICollection<UserRefreshToken>? UserRefreshTokens { get; set; }
    }
}
