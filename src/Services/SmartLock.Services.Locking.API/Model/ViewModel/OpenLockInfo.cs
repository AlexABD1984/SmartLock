using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.Services.Locking.API.Model.ViewModel
{
    public class OpenLockInfo
    {
        public OpenLockInfo(Guid userId, Guid lockId, string securityCode)
        {
            UserId = userId;
            LockId = lockId;
            SecurityCode = securityCode;
        }
        public Guid LockId { get; set; }
        public Guid UserId { get; set; }
        public string SecurityCode { get; set; }

    }
}
