using System;
using System.Threading.Tasks;

namespace SmartLock.Services.Locking.API.ApplicationServices
{
    public interface IUserAccessService
    {
         Task<bool> HasAccess(Guid userId, Guid lockId);
    }
}