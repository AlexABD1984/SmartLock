using SmartLock.Services.Locking.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.Services.Locking.API.ApplicationServices
{
    public interface ILockingService
    {
        Task<OpenLockResult> OpenLock(Guid id);
    }
}
