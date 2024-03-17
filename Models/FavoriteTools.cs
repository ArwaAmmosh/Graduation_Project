using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Graduation_Project.Models
{
    [PrimaryKey("UserId", "ToolId")]

    public class FavoriteTools
    {

        [ForeignKey("UserInformation")]
        public int UserId { get; set; }
        [ForeignKey("Tool")]
        public int ToolId { get; set; }
        public UserInformation user { get; set; }
        public Tool tool { get; set; }
    }
}
