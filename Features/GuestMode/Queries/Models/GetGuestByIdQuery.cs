
namespace Graduation_Project.Features.GuestMode.Queries.Models
{
    public class GetGuestByIdQuery:IRequest<Response<GetGuestByIdResponse>>
    {
        public int Id { get; set; }
    }
}
