using System.ComponentModel.DataAnnotations.Schema;

namespace Graduation_Project.Entities
{
    [PrimaryKey("ToolId", "UserId")]

    public class FavoriteTool
    {

        [ForeignKey("User")]
        public int UserId { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'User' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public User User { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'User' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

        [ForeignKey("Tool")]
        public int ToolId { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'Tool' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public Tool Tool { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Tool' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    }
}
