using System.ComponentModel.DataAnnotations.Schema;

namespace Graduation_Project.Entities
{
    public class ToolPhoto
    {
        [Key]
        public int ToolPhotoId { get; set; }

#pragma warning disable CS8618 // Non-nullable property 'ToolImages' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public byte[] ToolImages { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'ToolImages' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

        [ForeignKey("Tool")]
        public int ToolId { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'Tool' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public Tool Tool { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Tool' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    }
}
