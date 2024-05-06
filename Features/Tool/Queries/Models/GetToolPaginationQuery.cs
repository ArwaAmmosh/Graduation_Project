using Graduation_Project.Features.Tool.Queries.Results;
using Graduation_Project.Wrapper;

namespace Graduation_Project.Features.Tool.Queries.Models
{
    public class GetToolPaginationQuery : IRequest<PaginatedResult<GetToolPaginationReponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string[]? OrderBy { get; set; }
        public string? Search { get; set; }
    }
}