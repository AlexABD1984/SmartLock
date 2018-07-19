using Microsoft.EntityFrameworkCore;
using SmartLock.ClientApp.MVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.ClientApp.MVC
{
    public class AuditLogDbContext : DbContext
    {
        public AuditLogDbContext()
        {
        }
        public AuditLogDbContext(DbContextOptions<AuditLogDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = @"Server=.\;Database=AuditLog;Trusted_Connection=True;MultipleActiveResultSets=true";
            optionsBuilder
                .UseSqlServer(connectionString, providerOptions => providerOptions.CommandTimeout(60))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }

        public DbSet<SmartLock.ClientApp.MVC.Model.AuditLog> AuditLogs { get; set; }
    }
}
