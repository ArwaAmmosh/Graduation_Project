using Microsoft.EntityFrameworkCore;
namespace Graduation_Project.Models
{
    public class UNITOOLDbContext :DbContext
    {
        public UNITOOLDbContext (DbContextOptions<UNITOOLDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=UNITOOL;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserInformation> UsersInformations { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<ToolPhoto> Photos { get; set; }
        public DbSet<FavoriteTool> FavoriteTool { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure UserInformation entity
            modelBuilder.Entity<UserInformation>()
                .HasKey(ui => ui.UserInformationId);

            // Configure Tool entity
            modelBuilder.Entity<Tool>()
                .HasKey(t => t.ToolId);

            modelBuilder.Entity<Tool>()
                .HasOne(t => t.UserInformation)
                .WithMany(u => u.Tool)
                .HasForeignKey(t => t.UserInformationId);

            // Configure ToolPhoto entity
            modelBuilder.Entity<ToolPhoto>()
                .HasKey(tp => tp.ToolPhotoId);

            modelBuilder.Entity<Tool>()
                .HasMany(t => t.ToolPhoto)
                .WithOne(tp => tp.Tool)
                .HasForeignKey(tp => tp.ToolId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure FavoriteTool entity
            modelBuilder.Entity<FavoriteTool>()
                .HasKey(ft => new { ft.UserInformationId, ft.ToolId });

            modelBuilder.Entity<FavoriteTool>()
                .HasOne(ft => ft.UserInformation)
                .WithMany(ui => ui.FavoriteTool)
                .HasForeignKey(ft => ft.UserInformationId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FavoriteTool>()
                .HasOne(ft => ft.Tool)
                .WithMany(t => t.FavoriteTool)
                .HasForeignKey(ft => ft.ToolId)
                .OnDelete(DeleteBehavior.Cascade);




            base.OnModelCreating(modelBuilder);
        }
    }
    
         
}



