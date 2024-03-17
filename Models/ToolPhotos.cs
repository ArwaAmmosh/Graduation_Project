using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Graduation_Project.Models
{
    public class ToolPhotos
    {
        [Key]
        [ForeignKey("Tool")]
        public int Tool_Id { get; set; }
        public byte[] ToolImages { get; set; }
        public Tool tool { get; set; }
    }
}
