using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<InvalidatedToken> InvalidatedTokens { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(user =>
            {
                user.HasIndex(user => user.Username)
                    .IsUnique(true);
                user.Property(user => user.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                user.HasMany(user => user.Roles)
                .WithMany(role => role.Users)
                .UsingEntity<UserRole>();
            });

            modelBuilder.Entity<Role>(role =>
            {
                role.HasMany(role => role.Permissions)
                .WithMany(permission => permission.Roles)
                .UsingEntity<RolePermission>();
            });
        }
    }
}
