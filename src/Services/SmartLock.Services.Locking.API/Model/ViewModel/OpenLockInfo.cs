using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.Services.Locking.API.Model.ViewModel
{
    public class OpenLockInfo
    {
        public Guid LockId { get; set; }
        public Guid UserId { get; set; }
        public Guid SecurityCode { get; set; }

    }
}
