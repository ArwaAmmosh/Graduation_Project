namespace Graduation_Project.Features.Authorization.Queries.Models
{
    public class GetRoleByIdQuery:IRequest<Response<GetRoleByIdResult>>
    {
        public int Id { get; set; }
    }
}
