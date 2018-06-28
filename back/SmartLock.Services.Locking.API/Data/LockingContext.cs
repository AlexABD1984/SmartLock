//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using SmartLock.Services.Locking.API.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace SmartLock.Services.Locking.API.Data
//{
//    public class LockingContext : DbContext
//    {
//        public LockingContext(DbContextOptions<LockingContext> options) : base(options)
//        {
//        }
//        public DbSet<Access> AcessList { get; set; }

//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//            //////////builder.ApplyConfiguration(new CatalogBrandEntityTypeConfiguration());
//        }
//    }


//    public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<LockingContext>
//    {
//        public LockingContext CreateDbContext(string[] args)
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<LockingContext>()
//                .UseSqlServer("Server=.;Initial Catalog=Microsoft.eShopOnContainers.Services.CatalogDb;Integrated Security=true");

//            return new LockingContext(optionsBuilder.Options);
//        }
//    }
//}
