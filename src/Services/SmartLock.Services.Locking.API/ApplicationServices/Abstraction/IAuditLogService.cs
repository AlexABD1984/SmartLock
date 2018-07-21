using SmartLock.Services.Locking.API.Model;
using SmartLock.Services.Locking.API.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.Services.Locking.API.ApplicationServices
{
    public interface IAuditLogService
    {
        Task<Guid> LogLockRequest(Guid lockId, Guid userId, LockCommand command);
        Task<Guid> LogLockResult(Guid auditLogId, OpenLockResult result);
    }
}
