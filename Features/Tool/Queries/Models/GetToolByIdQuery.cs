
using Graduation_Project.Features.Tool.Queries.Results;

namespace Graduation_Project.Features.Tool.Queries.Models
{
    public class GetToolByIdQuery : IRequest<Response<GetToolByIdResponse>>
    {
        public int Id { get; set; }
        public GetToolByIdQuery(int id)
        {
            Id = id;
        }
    }
}