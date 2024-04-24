using Graduation_Project.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Graduation_Project.Entities
{
    public class Tool
    {
        [Key]
        public int ToolId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RentTime { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
        public string College { get; set; }
        public string Acadmicyear { get; set; }
        public string University { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public List<FavoriteTool> FavoriteTool { get; set; }
        public List<ToolPhoto> ToolPhoto { get; set; }
    }
}
