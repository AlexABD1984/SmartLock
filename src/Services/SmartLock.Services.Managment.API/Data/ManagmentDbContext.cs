using Microsoft.EntityFrameworkCore;
using SmartLock.Services.Managment.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.Services.Managment.API.Data
{
    public class ManagmentDbContext : DbContext
    {
        public ManagmentDbContext(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessRight>()
                .HasOne(p => p.User)
                .WithMany(b => b.AccessRights)
                .HasForeignKey(e => e.AccessorId);

            modelBuilder.Entity<AccessRight>()
                .HasOne(p => p.UserGroup)
                .WithMany(b => b.AccessRights)
                .HasForeignKey(e => e.AccessorId);
        }

        public DbSet<Lock> Locks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> Groups { get; set; }
        public DbSet<UserGroupMember> UserGroupMembers { get; set; }
        public DbSet<AccessRight> AccessRights { get; set; }
    }
}
    
