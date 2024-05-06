using Graduation_Project.Features.Tool.Queries.Results;

namespace Graduation_Project.Mapping.Tools
{
    public partial class MappingProfile
    {
        public void GetToolPaginationMapping()
        {
            CreateMap<Tool, GetToolPaginationReponse>();
        }
    }
}
