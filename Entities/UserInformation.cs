namespace Graduation_Project.Entities
{
    public class UserInformation
    {
        [Key]
        public int UserInformationId { get; set; }
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
        public byte[] PersonalImage { get; set; }
        public string NationalId { get; set; }
        public List<Tool> Tool { get; set; }
        public List<FavoriteTool> FavoriteTool { get; set; }


    }
}
