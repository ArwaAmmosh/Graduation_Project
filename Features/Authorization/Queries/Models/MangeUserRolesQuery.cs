namespace Graduation_Project.Features.Authorization.Queries.Models
{
    public class MangeUserRolesQuery:IRequest<Response<MangeUserRolesResult>>
    {
        public int UserId { get; set; }
    }
}
