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
        public string Department { get; set; }
        public string RentTime { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
        public string College { get; set; }
        public string AcademicYear { get; set; }
        public string University { get; set; }
        


        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<FavoriteTool> FavoriteTool { get; set; }
        public ICollection<ToolPhoto> ToolPhotos { get; set; }

    }
}
