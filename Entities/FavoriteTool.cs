using Graduation_Project.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Graduation_Project.Entities
{
   // [PrimaryKey("ToolId", "UserId")]

    public class FavoriteTool
    {
        
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Tool")]
        public int ToolId { get; set; }
        public Tool Tool { get; set; }
  }
}
