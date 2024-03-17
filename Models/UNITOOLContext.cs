using Microsoft.EntityFrameworkCore;
namespace Graduation_Project.Models
{
    public class UNITOOLContext :DbContext
    {
        public UNITOOLContext (DbContextOptions<UNITOOLContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-A0LMSG6\\SD;Database=UNITOOL;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        public DbSet<User> User { get; set; }
        public DbSet<UserInformation> UsersInformations { get; set; }
        public DbSet<Tool> Tool { get; set; }
        public DbSet<ToolPhotos> Photo { get; set; }
        public DbSet<FavoriteTools> FavoriteTool { get; set;}
        

    }
}
