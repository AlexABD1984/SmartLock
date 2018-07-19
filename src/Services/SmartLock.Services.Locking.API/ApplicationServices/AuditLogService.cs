using CacheManager.Core.Logging;
using Microsoft.Extensions.Logging;
using SmartLock.Services.AuditLogs.API.Model;
using SmartLock.Services.Locking.API.Infrastructure;
using SmartLock.Services.Locking.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.Services.Locking.API.ApplicationServices
{
    public class AuditLogService: IAuditLogService
    {
        private readonly AuditLogHttpClient _auditLogHttpClient;
        private readonly ILogger<AuditLogService> _logger = null;

        public AuditLogService(AuditLogHttpClient auditLogHttpClient,ILogger<AuditLogService> logger)
        {
            _auditLogHttpClient = auditLogHttpClient;
            _logger = logger;
        }
        public async Task<Guid> LogLockRequest(Guid lockId,Guid userId, LockCommand command)
        {
            Guid auditLogId = await _auditLogHttpClient.CallAuditLog(lockId, userId, command);
            return auditLogId;
        }
        public async Task<Guid> LogLockResult(Guid auditLogId, OpenLockResult result)
        {
            return await _auditLogHttpClient.UpdateAuditLog(auditLogId, result);            
        }
    }
}
