using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Graduation_Project.Models
{
    

    public class FavoriteTool
    {
        [Key]
        public int FavoriteToolId { get; set; }

        [ForeignKey("UserInformation")]
        public int UserInformationId { get; set; }
        public UserInformation UserInformation { get; set; }

        [ForeignKey("Tool")]
        public int ToolId { get; set; }
        public Tool Tool { get; set; }
    }
}
