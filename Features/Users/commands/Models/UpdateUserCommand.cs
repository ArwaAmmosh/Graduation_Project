namespace Graduation_Project.Features.Users.commands.Models
{
    public class UpdateUserCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Univserity { get; set; }
        public string Government { get; set; }
        public string AcadmicYear { get; set; }
        public string College { get; set; }
       /* public byte[]? FrontIdImage { get; set; }
        public byte[]? BackIdImage { get; set; }
        public byte[]? CollegeCardFrontImage { get; set; }
        public byte[]? CollegeCardBackImage { get; set; }
        public byte[]? PersonalImage { get; set; }*/
        public string NationalId { get; set; }
    }
}
