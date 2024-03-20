using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Graduation_Project.Models
{
    public class ToolPhoto
    {
        [Key]
        public int ToolPhotoId { get; set; }

        public byte[] ToolImages { get; set; }

        [ForeignKey("Tool")]
        public int ToolId { get; set; }
        public Tool Tool { get; set; }
    }
}
