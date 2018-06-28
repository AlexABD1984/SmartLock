using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.Services.Locking.API.Models
{
    public class Access
    {
        public Guid UserId { get; set; }
        public bool HasAccess { get; set; }
    }
}
