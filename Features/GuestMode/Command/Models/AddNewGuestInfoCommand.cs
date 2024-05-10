namespace Graduation_Project.Features.GuestMode.Command.Models
{
    public class AddNewGuestInfoCommand: IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? HowCanWeHelpYouMassage { get; set; }
    }
}
