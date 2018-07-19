using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.Services.Locking.API.Model
{
    public enum LockCommand
    {
        Open,
        Lock
    }
    public enum OpenLockResult
    {
        Opened,
        UserAccessDenied,
        OutfServce,
        LockError
    }
}
