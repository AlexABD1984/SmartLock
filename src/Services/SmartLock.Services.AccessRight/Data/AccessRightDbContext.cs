using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model=SmartLock.Services.AccessRight.Model;

namespace SmartLock.Services.AccessRight.Data
{
    public class AccessRightDbContext : DbContext
    {
        public AccessRightDbContext(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.AccessRight>().HasKey(table => new {
                table.UserId,
                table.LockId
            });
        }
        public DbSet<Model.AccessRight> AccessRights { get; set; }
    }
}
