using System.ComponentModel.DataAnnotations;

namespace Graduation_Project.Models
{
    public class UserInformation
    {
        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Government { get; set; }
        public string AcadmicYear { get; set; }
        public string College { get; set; }
        public string Phone { get; set; }
        public byte[] FrontIdImage { get; set; }
        public byte[] BackIdImage { get; set; }
        public byte[] CollegeCardFrontImage { get; set; }
        public byte[] CollegeCardBackImage { get; set; }
        public string NationalId { get; set; }
        public List<Tool> tool { get; set; }
        public List<FavoriteTools> favoriteTools { get; set; }
    }
}
