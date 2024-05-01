using Graduation_Project.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace Graduation_Project.Entities
{
    public class UNITOOLDbContext : IdentityDbContext<User, IdentityRole<int>, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public UNITOOLDbContext(DbContextOptions<UNITOOLDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=UNITOOL;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRefreshToken> RefreshTokens { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<ToolPhoto> Photos { get; set; }
        public DbSet<FavoriteTool> FavoriteTool { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configure Tool entity
            modelBuilder.Entity<Tool>()
                .HasKey(t => t.ToolId);

            modelBuilder.Entity<Tool>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tool)
                .HasForeignKey(t => t.UserId);

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
                .HasKey(ft => new { ft.UserId, ft.ToolId });

            modelBuilder.Entity<FavoriteTool>()
                .HasOne(ft => ft.User)
                .WithMany(ui => ui.FavoriteTool)
                .HasForeignKey(ft => ft.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FavoriteTool>()
                .HasOne(ft => ft.Tool)
                .WithMany(t => t.FavoriteTool)
                .HasForeignKey(ft => ft.ToolId)
                .OnDelete(DeleteBehavior.NoAction);




            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }


}



