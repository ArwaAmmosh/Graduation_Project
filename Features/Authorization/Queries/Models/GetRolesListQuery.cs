using Graduation_Project.Features.Authorization.Queries.Results;

namespace Graduation_Project.Features.Authorization.Queries.Models
{
    public class GetRolesListQuery :IRequest<Response<List<GetRolesListResult>>>
    {
    }
}
