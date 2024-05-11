using Graduation_Project.Bases;

namespace Graduation_Project.Features.Users.commands.Models
{
    public class UpdateUserCommand : IRequest<Response<string>>
    {
        public string University { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Government { get; set; }
        public string AcadmicYear { get; set; }
        public string College { get; set; }
        public string NationalId { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; } = default!;
        public IFormFile FrontIdImage { get; set; }
        public IFormFile BackIdImage { get; set; }
        public IFormFile CollegeCardFrontImage { get; set; }
        public IFormFile CollegeCardBackImage { get; set; }
        public IFormFile PersonalImage { get; set; }
    }
    
}
