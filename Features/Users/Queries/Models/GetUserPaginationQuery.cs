namespace Graduation_Project.Features.Users.Queries.Models
{
    public class GetUserPaginationQuery : IRequest<PaginatedResult<GetUserPaginationResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
