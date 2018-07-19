using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartLock.Services.Managment.API.Data;
using SmartLock.Services.Managment.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.Services.Managment.API.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ManagmentDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ManagmentDbContext>>()))
            {
                // Look for any Lock Info.
                if (context.Locks.Any())
                {
                    return;   // DB has been seeded
                }

                context.Locks.AddRange(
                     new Lock
                     {
                         LockId = Guid.NewGuid(),
                         Name = "Tunnel Door",
                     },

                     new Lock
                     {
                         LockId = Guid.NewGuid(),
                         Name = "Office Door",
                     }
                );
                var AliceUserId = Guid.NewGuid();
                var BobUserId = Guid.NewGuid();
                context.Users.AddRange(
                     new User
                     {
                         UserId = AliceUserId,
                         Name = "Alice",
                         Family = "Alice"
                     },

                     new User
                     {
                         UserId = BobUserId,
                         Name = "Bob",
                         Family = "Bob"
                     }
                );
                var ClayEmployeesGroupId = Guid.NewGuid();
                context.Groups.AddRange(
                    new UserGroup
                    {
                        UserGroupId = ClayEmployeesGroupId,
                        Name = "Clay Office Employees",
                    }
                );
                context.UserGroupMembers.AddRange(
                    new UserGroupMember
                    {
                        UserGroupMemberId = Guid.NewGuid(),
                        UserId = AliceUserId,
                        UserGroupId = ClayEmployeesGroupId,
                    },
                    new UserGroupMember
                    {
                        UserGroupMemberId = Guid.NewGuid(),
                        UserId = BobUserId,
                        UserGroupId = ClayEmployeesGroupId,
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
