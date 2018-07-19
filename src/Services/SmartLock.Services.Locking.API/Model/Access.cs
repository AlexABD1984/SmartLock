using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.Services.Locking.API.Model
{
    public class Access
    {
        public Guid UserId { get; set; }
        public bool HasAccess { get; set; }
    }
}
