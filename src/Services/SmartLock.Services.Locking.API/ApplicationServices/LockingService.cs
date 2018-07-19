using SmartLock.Services.Locking.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.Services.Locking.API.ApplicationServices
{
    public class LockingService : ILockingService
    {
        public Task<OpenLockResult> OpenLock(Guid id)
        {
            //Call Service or Lock SDK and Return Result
            return Task.FromResult<OpenLockResult>(OpenLockResult.Opened);
        }
    }
}
