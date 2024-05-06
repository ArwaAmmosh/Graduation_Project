namespace Graduation_Project.Mapping.Tools
{
    public partial class MappingProfile
    {
        public void AddToolMapping()
        {
            CreateMap<ToolPostDto, Tool>();

        }
    }
}
