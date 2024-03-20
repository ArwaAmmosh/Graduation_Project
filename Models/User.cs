using System.ComponentModel.DataAnnotations;

namespace Graduation_Project.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string? verficationToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpire { get; set; }

    
    }
}
