namespace Graduation_Project.Features.Authorization.Queries.Models
{
    public class GetRoleByIdQuery:IRequest<Response<GetUserByIdResult>>
    {
        public int Id { get; set; }
    }
}
