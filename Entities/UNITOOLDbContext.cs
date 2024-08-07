﻿using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Graduation_Project.Entities
{
    public class UNITOOLDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        private readonly IEncryptionProvider _encryptionProvider;
        public UNITOOLDbContext() 
        {
        }
        public UNITOOLDbContext(DbContextOptions<UNITOOLDbContext> options) : base(options)
        {
             _encryptionProvider = new GenerateEncryptionProvider("8a4dcaaec64d412380fe4b02193cd26f");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=db4959.public.databaseasp.net; Database=db4959; User Id=db4959; Password=M!i4?2JkN-a5; Encrypt=False; MultipleActiveResultSets=True; TrustServerCertificate=True");
        }
        public DbSet<UserRefreshToken> RefreshTokens { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<ToolPhoto> ToolPhotos { get; set; }
        public DbSet<FavoriteTool> FavoriteTool { get; set; }
        public DbSet<GuestModeUser> GuestModes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configure Tool entity
            modelBuilder.Entity<Tool>()
                .HasKey(t => t.ToolId);

            modelBuilder.Entity<Tool>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tool)
                .HasForeignKey(t => t.UserId);

            //Configure FavoriteTool entity
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
                .HasForeignKey(ft => ft.ToolId);

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.UseEncryption(_encryptionProvider);
        }
    }


}



