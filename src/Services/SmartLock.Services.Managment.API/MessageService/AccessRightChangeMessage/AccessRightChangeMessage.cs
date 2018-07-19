using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.Services.Managment.API.MessageService
{
    public class AccessRightChangeMessage
    {
        public Guid UserId { get; }

        public Guid LockId { get; }
        public bool HasAccess { get; }

        public AccessRightChangeMessage(Guid userId,Guid lockId,bool hasAccess )
        {
            UserId = userId;
            lockId = LockId;
            HasAccess = hasAccess;
        }
    }
}
