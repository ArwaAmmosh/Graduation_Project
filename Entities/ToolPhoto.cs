using System.ComponentModel.DataAnnotations.Schema;

namespace Graduation_Project.Entities
{
    public class ToolPhoto
    {
        [Key]
        public int ToolPhotoId { get; set; }

        public string? ToolImages { get; set; }


        [ForeignKey("Tool")]
        public int ToolId { get; set; }
        public Tool Tool { get; set; }

    }
}
