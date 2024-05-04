namespace Graduation_Project.Features.Authorization.Queries.Models
{
    public class MangeUserClaimsQuery:IRequest<Response<MangeUserClaimsResult>>
    {
        public int UserId { get; set; }
    }
}
