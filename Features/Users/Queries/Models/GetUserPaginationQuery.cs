

using Graduation_Project.Features.Users.Queries.Results;
using Graduation_Project.Wrapper;

namespace Graduation_Project.Features.Users.Queries.Models
{
    public class GetUserPaginationQuery : IRequest<PaginatedResult<GetUserPaginationReponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
