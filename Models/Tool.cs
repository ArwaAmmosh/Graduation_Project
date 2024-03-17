using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Graduation_Project.Models
{
    public class Tool
    {
        [Key]
        public int ToolID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RentTime { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
        public string Acadmicyear { get; set; }
        public string University { get; set; }
        [ForeignKey("UserInformation")]
        public int UserId { get; set; }
        public UserInformation user { get; set; }
        public List<FavoriteTools> favoriteTools { get; set; }
    }
}
