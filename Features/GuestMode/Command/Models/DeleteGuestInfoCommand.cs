namespace Graduation_Project.Features.GuestMode.Command.Models
{
    public class DeleteGuestInfoCommand:IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
